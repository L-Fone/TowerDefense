//
// UpdateScreen.cs
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace libx
{
    public class UpdateScreen : MonoBehaviour, IUpdater
    {
        public Text version;
        public Slider progressBar;
        public Text progressText;
        public Button btnStart;
        public Button btnClear;

        private void Start()
        {
            version.text = "APP: 4.0\nRES：1";
            var updater = Game.Scene.GetComponent<Updater>();
            updater.listener = this;


            btnStart.onClick.AddListener(() =>
            {
                updater.StartUpdate();
            });

            btnClear.onClick.AddListener(() =>
            {
                updater.OnClear();
            });
        }
        private void DelRes(string dir)
        {
               foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                    {
                        FileInfo fi = new FileInfo(d);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            fi.Attributes = FileAttributes.Normal;
                        File.Delete(d);//直接删除其中的文件 
                    }
                    else
                    {
                        DirectoryInfo d1 = new DirectoryInfo(d);
                        if (d1.GetFiles().Length != 0)
                        {
                            DelRes(d1.FullName);////递归删除子文件夹
                        }
                        Directory.Delete(d, true);
                    }
                }
        }
        private void DelSomeRes(string path, int count)
        {
#if UNITY_STANDALONE
               path += "/TTL";
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            List<FileInfo> list = new List<FileInfo>(files);
            list.Sort(new Comparison<FileInfo>(delegate (FileInfo a, FileInfo b)
            {
                return -a.LastWriteTime.CompareTo(b.LastWriteTime);

            }));
            foreach (FileInfo f in list)
            {
                if (count <= 0) return;
                count--;
                File.Delete(f.FullName);
            }
#endif

        }
#region IUpdateManager implementation

        public void OnStart()
        {
            btnStart.gameObject.SetActive(false);
        }

        public void OnMessage(string msg)
        {
            progressText.text = msg;
        }

        public void OnProgress(float progress)
        {
            progressBar.value = progress;
        }

        public void OnVersion(string ver)
        {
            version.text = $"客户端版本号：{GlobalConfigComponent.Instance.GlobalProto.ClientVersion}\nres:{ver}";
        }


        public void OnClear()
        {
            btnStart.gameObject.SetActive(true);
        }
#endregion
    }
}