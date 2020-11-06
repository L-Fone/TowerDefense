//
// AssetsMenuItem.cs
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
using ETEditor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.Experimental;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace libx
{
    public static class MenuItems
    {
        private const string KApplyBuildRules = "Assets/Bundles/Build Rules";
        private const string KCopyAssetBundles = "Assets/Bundles/Copy Bundles";
        private const string KCopyAssetBundlesWithVFS = "Assets/Bundles/Copy Bundles VFS";
        private const string KBuildAssetBundles = "Assets/Bundles/Build Bundles";
        private const string KBuildPlayer = "Assets/Bundles/Build Player";
        private const string KViewDataPath = "Assets/Bundles/View Bundles";
        private const string KCopyBundles = "Assets/Bundles/Copy Bundles";

        //[MenuItem("Assets/Apply Rule/Text", false, 1)]
        //private static void ApplyRuleText()
        //{
        //    var rules = BuildScript.GetBuildRules();
        //    AddRulesForSelection(rules, rules.searchPatternText);
        //}

        //[MenuItem("Assets/Apply Rule/Prefab", false, 1)]
        //private static void ApplyRulePrefab()
        //{
        //    var rules = BuildScript.GetBuildRules();
        //    AddRulesForSelection(rules, rules.searchPatternPrefab);
        //}

        //[MenuItem("Assets/Apply Rule/Png", false, 1)]
        //private static void ApplyRulePng()
        //{
        //    var rules = BuildScript.GetBuildRules();
        //    AddRulesForSelection(rules, rules.searchPatternPng);
        //}

        //[MenuItem("Assets/Apply Rule/Material", false, 1)]
        //private static void ApplyRuleMaterial()
        //{
        //    var rules = BuildScript.GetBuildRules();
        //    AddRulesForSelection(rules, rules.searchPatternMaterial);
        //}

        //[MenuItem("Assets/Apply Rule/Controller", false, 1)]
        //private static void ApplyRuleController()
        //{
        //    var rules = BuildScript.GetBuildRules();
        //    AddRulesForSelection(rules, rules.searchPatternController);
        //}

        //[MenuItem("Assets/Apply Rule/Asset", false, 1)]
        //private static void ApplyRuleAsset()
        //{
        //    var rules = BuildScript.GetBuildRules();
        //    AddRulesForSelection(rules, rules.searchPatternAsset);
        //}

        //[MenuItem("Assets/Apply Rule/Scene", false, 1)]
        //private static void ApplyRuleScene()
        //{
        //    var rules = BuildScript.GetBuildRules();
        //    AddRulesForSelection(rules, rules.searchPatternScene);
        //} 
        [MenuItem(KCopyAssetBundles)]
        private static void CopyAssetBundles()
        {
            BuildScript.CopyAssetBundlesTo(Application.streamingAssetsPath);
            Log.Info($"复制成功");
        }

        [MenuItem(KCopyAssetBundlesWithVFS)]
        private static void CopyAssetBundlesWithVFS()
        {
            BuildScript.CopyAssetBundlesTo(Application.streamingAssetsPath, true);
            Log.Info($"复制成功");
        }

        [MenuItem("Assets/Apply Rule/Path", false, 1)]
        private static void ApplyRulePath()
        {
            var rules = BuildScript.GetBuildRules();
            AddRulesForPath(rules, rules.searchPatternDir);
        }
        [MenuItem("Assets/Apply Rule/Directory", false, 1)]
        private static void ApplyRuleDir()
        {
            var rules = BuildScript.GetBuildRules();
            AddRulesForDir(rules, rules.searchPatternDir);
        }
        [MenuItem("Assets/Apply Rule/TopDirectory", false, 1)]
        private static void ApplyRuleTopDir()
        {
            var rules = BuildScript.GetBuildRules();
            AddRulesForTopDir(rules, rules.searchPatternDir);
        }


        private static void AddRulesForSelection(BuildRules rules, string searchPattern)
        {
            var isDir = rules.searchPatternDir.Equals(searchPattern);
            foreach (var item in Selection.objects)
            {
                var path = AssetDatabase.GetAssetPath(item);
                var rule = new BuildRule
                {
                    searchPath = path,
                    searchPattern = searchPattern,
                    nameBy = isDir ? NameBy.Directory : NameBy.Path
                };
                ArrayUtility.Add(ref rules.rules, rule);
            }

            EditorUtility.SetDirty(rules);
            AssetDatabase.SaveAssets();
        }
        private static void AddRulesForTopDir(BuildRules rules, string searchPattern)
        {
            string[] strs = Selection.assetGUIDs;
            foreach (var item in strs)
            {
                string dirPath = AssetDatabase.GUIDToAssetPath(item);
                if (string.IsNullOrEmpty(dirPath) || !Directory.Exists(dirPath))
                {
                    return;
                }
                var rule = new BuildRule
                {
                    searchPath = dirPath,
                    searchPattern = searchPattern,
                    nameBy = NameBy.TopDirectory
                };
                bool isDistan = false;
                foreach (var r in rules.rules)
                {
                    if (r.searchPath == rule.searchPath)
                    {
                        ET.Log.Error($"重复：{r.searchPath }");
                        isDistan = true;
                        break;
                    }
                }
                if (isDistan) continue;
                ArrayUtility.Add(ref rules.rules, rule);

            }

            EditorUtility.SetDirty(rules);
            AssetDatabase.SaveAssets();
        }
        private static void AddRulesForDir(BuildRules rules, string searchPattern)
        {
            string[] strs = Selection.assetGUIDs;
            foreach (var item in strs)
            {
                string dirPath = AssetDatabase.GUIDToAssetPath(item);
                if (string.IsNullOrEmpty(dirPath) || !Directory.Exists(dirPath))
                {
                    return;
                }
                var rule = new BuildRule
                {
                    searchPath = dirPath,
                    searchPattern = searchPattern,
                    nameBy = NameBy.Directory
                };
                bool isDistan = false;
                foreach (var r in rules.rules)
                {
                    if (r.searchPath == rule.searchPath)
                    {
                        ET.Log.Error($"重复：{r.searchPath }");
                        isDistan = true;
                        break;
                    }
                }
                if (isDistan) continue;
                ArrayUtility.Add(ref rules.rules, rule);

            }

            EditorUtility.SetDirty(rules);
            AssetDatabase.SaveAssets();
        }
        private static void AddRulesForPath(BuildRules rules, string searchPattern)
        {
            string[] strs = Selection.assetGUIDs;
            foreach (var item in strs)
            {
                string dirPath = AssetDatabase.GUIDToAssetPath(item);
                if (string.IsNullOrEmpty(dirPath) || !Directory.Exists(dirPath))
                {
                    return;
                }
                var rule = new BuildRule
                {
                    searchPath = dirPath,
                    searchPattern = searchPattern,
                    nameBy = NameBy.Path
                };
                bool isDistan = false;
                foreach (var r in rules.rules)
                {
                    if (r.searchPath == rule.searchPath)
                    {
                        ET.Log.Error($"重复：{r.searchPath }");
                        isDistan = true;
                        break;
                    }
                }
                if (isDistan) continue;
                ArrayUtility.Add(ref rules.rules, rule);

            }


            EditorUtility.SetDirty(rules);
            AssetDatabase.SaveAssets();
        }
        [MenuItem(KApplyBuildRules)]
        private static void ApplyBuildRules()
        {
            var watch = new Stopwatch();
            watch.Start();
            BuildScript.ApplyBuildRules();
            watch.Stop();
            Debug.Log("ApplyBuildRules " + watch.ElapsedMilliseconds + " ms.");
        }

        [MenuItem(KBuildAssetBundles)]
        public static void BuildAssetBundles()
        {
            var watch = new Stopwatch();
            watch.Start();


            BuildScript.ApplyBuildRules();
            BuildScript.BuildAssetBundles();
            watch.Stop();
            Debug.Log("BuildAssetBundles " + watch.ElapsedMilliseconds + " ms.");
        }

        [MenuItem(KBuildPlayer)]
        private static void BuildStandalonePlayer()
        {
            BuildScript.BuildStandalonePlayer();
        }

        [MenuItem(KViewDataPath)]
        private static void ViewDataPath()
        {
            EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
        }

#if !UNITY_2018_1_OR_NEWER
        private const string KCopyPath = "Assets/Copy Path";
        [MenuItem(KCopyPath)]
        private static void CopyPath()
        {
            var assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            EditorGUIUtility.systemCopyBuffer = assetPath;
            Debug.Log(assetPath);
        }
#endif
        private const string KToJson = "Assets/ToJson";

        [MenuItem(KToJson)]
        private static void ToJson()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var json = JsonUtility.ToJson(Selection.activeObject);
            File.WriteAllText(path.Replace(".asset", ".json"), json);
            AssetDatabase.Refresh();
        }

        #region Tools 
        [MenuItem("Tools/View CRC")]
        private static void GetCRC()
        {
            var path = EditorUtility.OpenFilePanel("OpenFile", Environment.CurrentDirectory, "");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            using (var fs = File.OpenRead(path))
            {
                var crc = Utilitys.GetCRC32Hash(fs);
                Debug.Log(crc);
            }
        }

        [MenuItem("Tools/View MD5")]
        private static void GetMD5()
        {
            var path = EditorUtility.OpenFilePanel("OpenFile", Environment.CurrentDirectory, "");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            using (var fs = File.OpenRead(path))
            {
                var crc = Utilitys.GetMD5Hash(fs);
                Debug.Log(crc);
            }
        }

        [MenuItem("Tools/Take a Screenshot")]
        private static void Screenshot()
        {
            var path = EditorUtility.SaveFilePanel("截屏", null, "screenshot_", "png");
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            ScreenCapture.CaptureScreenshot(path);

        }
        #endregion 
    }
}