//
// Updater.cs
//
// Author:
//       fjy <jiyuan.feng@live.com>
//
// Copyright (c) 2020 fjy
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using ET;
using libx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public interface IUpdater
{
    void OnStart();

    void OnMessage(string msg);

    void OnProgress(float progress);

    void OnVersion(string ver);

    void OnClear();
}

public class UpdaterAwakeSystem : AwakeSystem<Updater>
{
    public override void Awake(Updater self)
    {
        self.Awake();
    }
}
public class UpdaterDestroySystem : DestroySystem<Updater>
{
    public override void Destroy(Updater self)
    {
        self.Destroy();
    }
}
//public class UpdaterUpdateSystem : UpdateSystem<Updater>
//{
//    public override void Update(Updater self)
//    {
//        self.Update();
//    }
//}

public class Updater : Entity, IUpdater, INetworkMonitorListener
{
    enum Step
    {
        Wait,
        Copy,
        Coping,
        Versions,
        Prepared,
        Download,
    }

    private Step _step;

    //[SerializeField] private string gameScene = "Scene_Login.unity";
    [SerializeField] private bool enableVFS = true;
    [SerializeField] private bool development;

    public IUpdater listener { get; set; }

    private Downloader _downloader;
    private NetworkMonitor _monitor;
    private string _platform;
    private string _savePath;
    private List<VFile> _versions = new List<VFile>();

    //private bool update;

    public void OnMessage(string msg)
    {
        if (listener != null)
        {
            listener.OnMessage(msg);
        }
    }

    public void OnProgress(float progress)
    {
        if (listener != null)
        {
            listener.OnProgress(progress);
        }
    }

    public void OnVersion(string ver)
    {
        if (listener != null)
        {
            listener.OnVersion(ver);
        }
    }

    public void Awake()
    {
        Game.Scene.AddComponentNoPool<Assets>();
        _downloader = Game.Scene.AddComponentNoPool<Downloader>();
        _monitor = Game.Scene.AddComponentNoPool<NetworkMonitor>();
        _downloader.onUpdate = OnUpdate;
        _downloader.onFinished = OnComplete;

        development = Define.IsEditorMode;

        _monitor.listener = this;

        _savePath = string.Format("{0}/TTL/", Application.persistentDataPath);
        if (Directory.Exists(_savePath))
            if (ET.Utility.FileOpation.GetFiles(_savePath, "*.bundle", SearchOption.AllDirectories).Length > 0)
            {
                ET.Utility.FileOpation.ClearDirectory(_savePath);
            }


        _platform = GetPlatformForAssetBundles(Application.platform);
        Log.Info($"平台:{_platform}");

        _step = Step.Wait;

        Assets.updatePath = _savePath;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (_reachabilityChanged || _step == Step.Wait)
        {
            return;
        }

        if (hasFocus)
        {
            Game.EventSystem.Publish(new ET.EventType.MessageBox_CloseAll
            {

            }).Coroutine();
            if (_step == Step.Download)
            {
                _downloader.Restart();
            }
            else
            {
                StartUpdate();
            }
        }
        else
        {
            if (_step == Step.Download)
            {
                _downloader.Stop();
            }
        }
    }

    private bool _reachabilityChanged;

    public void OnReachablityChanged(NetworkReachability reachability)
    {
        if (_step == Step.Wait)
        {
            return;
        }

        _reachabilityChanged = true;
        if (_step == Step.Download)
        {
            _downloader.Stop();
        }

        if (reachability == NetworkReachability.NotReachable)
        {
            Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
            {
                title = "提示！",
                content = "找不到网络，请确保手机已经联网",
                no = "退出",
                action = (MessageBoxEventId id) =>
                {
                    if (id == MessageBoxEventId.Ok)
                    {
                        if (_step == Step.Download)
                        {
                            _downloader.Restart();
                        }
                        else
                        {
                            StartUpdate();
                        }
                        _reachabilityChanged = false;
                    }
                    else
                    {
                        Quit();
                    }
                },

            }).Coroutine();

        }
        else
        {
            if (_step == Step.Download)
            {
                _downloader.Restart();
            }
            else
            {
                StartUpdate();
            }
            _reachabilityChanged = false;
            Game.EventSystem.Publish(new ET.EventType.MessageBox_CloseAll
            {

            }).Coroutine();
        }
    }

    private void OnUpdate(long progress, long size, float speed)
    {
        OnMessage(string.Format("下载中...{0}/{1}, 速度：{2}",
            Downloader.GetDisplaySize(progress),
            Downloader.GetDisplaySize(size),
            Downloader.GetDisplaySpeed(speed)));

        OnProgress(progress * 1f / size);
    }

    public void Clear()
    {
        Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
        {
            title = "提示！",
            content = "清除数据后所有数据需要重新下载，请确认！",
            ok = "清除",
            action = (MessageBoxEventId id) =>
            {
                if (id != MessageBoxEventId.Ok)
                    return;
                OnClear();
            },

        }).Coroutine();
    }

    public void OnClear()
    {
        OnMessage("数据清除完毕");
        OnProgress(0);
        _versions.Clear();
        _downloader.Clear();
        _step = Step.Wait;
        _reachabilityChanged = false;

        Assets.Clear();

        if (listener != null)
        {
            listener.OnClear();
        }

        if (Directory.Exists(_savePath))
        {
            Directory.Delete(_savePath, true);
        }
    }

    public void OnStart()
    {
        if (listener != null)
        {
            listener.OnStart();
        }
    }

    private IEnumerator _checking;

    public void StartUpdate()
    {
        Debug.Log("StartUpdate.Development:" + development);
#if UNITY_EDITOR
        if (development)
        {
            Assets.runtimeMode = false;
            LoadGameScene();
            return;
        }
#endif
        OnStart();
        Update().Coroutine();
    }

    private void AddDownload(VFile item)
    {
        _downloader.AddDownload(GetDownloadURL(item.name), item.name, _savePath + item.name, item.hash, item.len);
    }

    private void PrepareDownloads()
    {
        if (enableVFS)
        {
            var path = string.Format("{0}{1}", _savePath, Versions.Dataname);
            if (!File.Exists(path))
            {
                AddDownload(_versions[0]);
                return;
            }

            Versions.LoadDisk(path);
        }

        for (var i = 1; i < _versions.Count; i++)
        {
            var item = _versions[i];
            if (Versions.IsNew(string.Format("{0}{1}", _savePath, item.name), item.len, item.hash))
            {
                AddDownload(item);
            }
        }
    }

    private async ETTask RequestVFS()
    {
        enableVFS = true;
        await ETTask.CompletedTask;
    }

    private static string GetPlatformForAssetBundles(RuntimePlatform target)
    {
        // ReSharper disable once SwitchStatementMissingSomeCases
        switch (target)
        {
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.WebGLPlayer:
                return "WebGL";
            case RuntimePlatform.WindowsPlayer:
                return "Windows";
            case RuntimePlatform.WindowsEditor:
#if PC
                return "Windows";
#elif ANDROID
                return "Android";
#endif
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                return "iOS"; // OSX
            default:
                return null;
        }
    }

    private string GetDownloadURL(string filename)
    {
        return string.Format("{0}{1}/{2}", GlobalConfigComponent.Instance.GlobalProto.GetUrl(), _platform, filename);
    }

    public async ETVoid Update()
    {
        if (!Directory.Exists(_savePath))
        {
            Directory.CreateDirectory(_savePath);
        }

        if (_step == Step.Wait)
        {
            await RequestVFS();
            _step = Step.Copy;
            Update().Coroutine();
            return;
        }

        if (_step == Step.Copy)
        {
            Log.Info($"Update3");
            await RequestCopy();
            Update().Coroutine();
            return;
        }

        if (_step == Step.Coping)
        {
            Log.Info($"Update4");
            var path = Path.Combine(_savePath, Versions.Filename + ".tmp");
            var versions = Versions.LoadVersions(path);
            var basePath = GetBasePath();
            await UpdateCopy(versions, basePath);
            _step = Step.Versions;
            Update().Coroutine();
            return;
        }

        if (_step == Step.Versions)
        {
            Log.Info($"Update5");
            await RequestVersions();
            Update().Coroutine();
            return;
        }

        if (_step == Step.Prepared)
        {
            Log.Info($"Update6");
            OnMessage("正在检查版本信息...");
            var totalSize = _downloader.size;
            if (totalSize > 0)
            {
                var tips = string.Format("发现内容更新，总计需要下载 {0} 内容", Downloader.GetDisplaySize(totalSize));
                Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
                {
                    title = "提示！",
                    content = tips,
                    ok = "下载",
                    no = "退出",
                    action = (MessageBoxEventId id) =>
                    {
                        if (id == MessageBoxEventId.Ok)
                        {
                            _downloader.StartDownload();
                            _step = Step.Download;
                        }
                        else
                        {
                            Quit();
                        }
                    }
                }).Coroutine();
                return;

            }
            else
            {
                OnComplete();
            }
        }
    }

    private async ETTask RequestVersions()
    {
        OnMessage("正在获取版本信息...");
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
            {
                title = "提示！",
                content = "请检查网络连接状态",
                ok = "重试",
                no = "退出",
                action = (MessageBoxEventId id) =>
                {
                    if (id == MessageBoxEventId.Ok)
                    {
                        StartUpdate();
                    }
                    else
                    {
                        Quit();
                    }
                }
            }).Coroutine();
            return;
        }

        var request = UnityWebRequest.Get(GetDownloadURL(Versions.Filename));
        request.downloadHandler = new DownloadHandlerFile(Path.Combine(_savePath, Versions.Filename));
        await SendWebRequest(request);
        var error = request.error;
        request.Dispose();
        if (!string.IsNullOrEmpty(error))
        {
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
            {
                title = "提示！",
                content = string.Format("获取服务器版本失败：{0}", error),
                ok = "重试",
                no = "退出",
                action = (MessageBoxEventId id) =>
                {
                    if (id == MessageBoxEventId.Ok)
                    {
                        StartUpdate();
                        tcs.SetResult();
                    }
                    else
                    {
                        Quit();
                        tcs.SetResult();
                    }
                }
            }).Coroutine();
            await tcs.Task;
            return;
        }
        try
        {
            _versions = Versions.LoadVersions(Path.Combine(_savePath, Versions.Filename), true);
            if (_versions.Count > 0)
            {
                PrepareDownloads();
                _step = Step.Prepared;
            }
            else
            {
                OnComplete();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            ETTaskCompletionSource tcs = new ETTaskCompletionSource();
            Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
            {
                title = "提示！",
                content = "版本文件加载失败",
                ok = "重试",
                no = "退出",
                action = (MessageBoxEventId id) =>
                {
                    if (id == MessageBoxEventId.Ok)
                    {
                        tcs.SetResult();
                        StartUpdate();
                    }
                    else
                    {
                        Quit();
                        tcs.SetResult();
                    }
                },

            }).Coroutine();
            await tcs.Task;
        }
    }

    private static string GetBasePath()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            return Application.streamingAssetsPath/*+ "/"*/;
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer ||
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            return "file:///" + Application.streamingAssetsPath + "/";
        }

        return "file://" + Application.streamingAssetsPath + "/";
    }
    private async ETTask RequestCopy()
    {
        int v1 = Versions.LoadVersion(Path.Combine(_savePath, Versions.Filename));
        var basePath = GetBasePath();
        var request = UnityWebRequest.Get(Path.Combine(basePath, Versions.Filename));
        var path = _savePath + Versions.Filename + ".tmp";
        request.downloadHandler = new DownloadHandlerFile(path);
        await SendWebRequest(request);
        if (string.IsNullOrEmpty(request.error))
        {
            var v2 = Versions.LoadVersion(path);
            if (v2 > v1)
            {
                ETTaskCompletionSource tcs = new ETTaskCompletionSource();
                Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
                {
                    title = "提示！",
                    content = "是否将资源解压到本地？",
                    ok = "解压",
                    no = "跳过",
                    action = (MessageBoxEventId id) =>
                    {
                        if (id == MessageBoxEventId.Ok)
                        {
                            _step = Step.Coping;
                            tcs.SetResult();
                        }
                        else
                        {
                            _step = Step.Versions;
                            tcs.SetResult();
                        }
                    },

                }).Coroutine();
                await tcs.Task;
            }
            else
            {
                Log.Info($"存在.tmp");
                Versions.LoadVersions(path);
                _step = Step.Versions;
            }
        }
        else
        {
            Log.Info($"不存在.tmp");
            _step = Step.Versions;
        }
        request.Dispose();

    }


    private async ETTask SendWebRequest(UnityWebRequest request)
    {
        ETTaskCompletionSource tcs = new ETTaskCompletionSource();
        var option = request.SendWebRequest();
        option.completed += (obj) =>
        {
            tcs.SetResult();
        };
        await tcs.Task;
    }
    private async ETTask UpdateCopy(IList<VFile> versions, string basePath)
    {
        var version = versions[0];
        Log.Info($"{version.name}");
        if (version.name.Equals(Versions.Dataname))
        {
            var request = UnityWebRequest.Get(Path.Combine(basePath, version.name));
            request.downloadHandler = new DownloadHandlerFile(_savePath + version.name);
            Log.Info($"url:{request.url}");
            await SendWebRequest(request);
            if (string.IsNullOrEmpty(request.error))
            {
                Log.Info($"{request.error}");
            }
            //if (!req.isDone)
            //{
            //    OnMessage("正在复制文件");
            //    OnProgress(req.progress);
            //    return;
            //}
            request.Dispose();
        }
        else
        {
            for (var index = 0; index < versions.Count; index++)
            {
                var item = versions[index];
                var request = UnityWebRequest.Get(Path.Combine(basePath, item.name));
                request.downloadHandler = new DownloadHandlerFile(Path.Combine(_savePath, item.name));
                Log.Info($"url:{request.url}");
                await SendWebRequest(request);
                if (string.IsNullOrEmpty(request.error))
                {
                    Log.Info($"{request.error}");
                }
                request.Dispose();
                OnMessage(string.Format("正在复制文件：{0}/{1}", index, versions.Count));
                OnProgress(index * 1f / versions.Count);
            }
        }
    }

    private void OnComplete()
    {
        if (enableVFS)
        {
            var dataPath = _savePath + Versions.Dataname;
            var downloads = _downloader.downloads;
            if (downloads.Count > 0 && File.Exists(dataPath))
            {
                OnMessage("更新本地版本信息");
                var files = new List<VFile>(downloads.Count);
                foreach (var download in downloads)
                {
                    files.Add(new VFile
                    {
                        name = download.name,
                        hash = download.hash,
                        len = download.len,
                    });
                }

                var file = files[0];
                if (!file.name.Equals(Versions.Dataname))
                {
                    Versions.UpdateDisk(dataPath, files);
                }
            }

            Versions.LoadDisk(dataPath);
        }

        OnProgress(1);
        OnMessage("更新完成");
        var version = Versions.LoadVersion(Path.Combine(_savePath, Versions.Filename));
        if (version > 0)
        {
            OnVersion("资源版本号: v" + Application.version + "res" + version.ToString());
        }

        LoadGameScene();
    }

    private void LoadGameScene()
    {
        OnMessage("正在初始化");
        var init = Assets.Initialize();
        init.completed += request =>
        {
            if (string.IsNullOrEmpty(init.error))
            {
                Assets.AddSearchPath("Assets/Download/Scenes");
                init.Release();
                OnProgress(0);
                OnMessage("加载游戏场景");
                Game.EventSystem.Publish(new ET.EventType.DownloadInitResourceFinish
                {
                    zoneScene = Game.Scene.Get(1)
                }).Coroutine();
            }
            else
            {
                init.Release();
                Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
                {
                    title = "提示！",
                    content = "初始化异常错误：" + init.error + "请联系技术支持",
                    action = (MessageBoxEventId id) =>
                    {
                        Quit();
                    },

                }).Coroutine();
            }
        };

    }

    public void Destroy()
    {
        Game.EventSystem.Publish(new ET.EventType.MessageBox_Dispose
        {

        }).Coroutine();
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

