using System;
using System.IO;
using ET;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace ETEditor
{
    [InitializeOnLoad]
    public class Startup
    {
        private const string ScriptAssembliesDir = "Library/ScriptAssemblies";
        private const string CodeDir = "Assets/Download/Config/";
        private const string HotfixDll = "Unity.Hotfix.dll";
        private const string HotfixPdb = "Unity.Hotfix.pdb";
        private const string HotfixViewDll = "Unity.HotfixView.dll";
        private const string HotfixViewPdb = "Unity.HotfixView.pdb";

        static Startup()
        {
            if (Application.dataPath.Contains("CTTBuild")) return;
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixDll), Path.Combine(CodeDir, "Hotfix.dll.bytes"), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixPdb), Path.Combine(CodeDir, "Hotfix.pdb.bytes"), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixViewDll), Path.Combine(CodeDir, "HotfixView.dll.bytes"), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, HotfixViewPdb), Path.Combine(CodeDir, "HotfixView.pdb.bytes"), true);
            Log.Info($"复制Hotfix.dll, Hotfix.pdb到Download/Config完成");
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif
        }
    }
}