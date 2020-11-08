//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using Path = System.IO.Path;

namespace ET
{
    /// <summary>
    /// 实用函数集。
    /// </summary>
    public static partial class Utility
    {
        public static class FileOpation
        {
            public static void WriteBytes(string path,byte[] buffer)
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
            public static void WriteString(string path, string str)
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using(var sw =new StreamWriter(fs))
                    {
                        sw.Write(str);
                    }
                }
            }
            public static FileStream Create(string path)
            {
                if (File.Exists(path))
                {
                    return new FileStream(path, FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite);
                }
                return File.Create(path);
            }
            public static void Delete(string path)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            public static DirectoryInfo CreateDirectory(string path)
            {
                if (Directory.Exists(path))
                {
                     return new DirectoryInfo(path);
                }
                return Directory.CreateDirectory(path);
            }
            public static void DeleteDirectory(string path,bool recursive=false)
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path,recursive);
                }
            }
            public static void ClearDirectory(string path)
            {
                foreach (var info in GetFiles(path,"*.*", SearchOption.AllDirectories))
                {
                    Delete(info.FullName);
                }
            }


            public static FileInfo[] GetFiles(string path,string searchPattern,SearchOption searchOption)
            {
                if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                {
                    Log.Error($"path 文件夹不存在");
                    return null;
                }
                DirectoryInfo info = new DirectoryInfo(path);
                return info.GetFiles(searchPattern,searchOption);
            }
            public static void GetAllFiles(List<string> files, string dir)
            {
                string[] fls = Directory.GetFiles(dir);
                foreach (string fl in fls)
                {
                    files.Add(fl);
                }

                string[] subDirs = Directory.GetDirectories(dir);
                foreach (string subDir in subDirs)
                {
                    GetAllFiles(files, subDir);
                }
            }

            public static void CleanDirectory(string dir)
            {
                foreach (string subdir in Directory.GetDirectories(dir))
                {
                    Directory.Delete(subdir, true);
                }

                foreach (string subFile in Directory.GetFiles(dir))
                {
                    File.Delete(subFile);
                }
            }

            public static void CleanDirectory(string srcDir, string extensionName)
            {
                if (Directory.Exists(srcDir))
                {
                    string[] fls = Directory.GetFiles(srcDir);

                    foreach (string fl in fls)
                    {
                        if (fl.EndsWith(extensionName))
                        {
                            File.Delete(fl);
                        }
                    }

                    string[] subDirs = Directory.GetDirectories(srcDir);

                    foreach (string subDir in subDirs)
                    {
                        CleanDirectory(subDir, extensionName);
                    }
                }
            }

            public static bool CopyFile(string sourcePath, string targetPath, bool overwrite)
            {
                string sourceText = null;
                string targetText = null;

                if (File.Exists(sourcePath))
                {
                    sourceText = File.ReadAllText(sourcePath);
                }

                if (File.Exists(targetPath))
                {
                    targetText = File.ReadAllText(targetPath);
                }

                if (sourceText != targetText && File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, targetPath, overwrite);
                    return true;
                }

                return false;
            }

            public static void CopyDirectory(string srcDir, string tgtDir)
            {
                DirectoryInfo source = new DirectoryInfo(srcDir);
                DirectoryInfo target = new DirectoryInfo(tgtDir);

                if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new Exception("父目录不能拷贝到子目录！");
                }

                if (!source.Exists)
                {
                    return;
                }

                if (!target.Exists)
                {
                    target.Create();
                }

                FileInfo[] files = source.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    File.Copy(files[i].FullName, Path.Combine(target.FullName, files[i].Name), true);
                }

                DirectoryInfo[] dirs = source.GetDirectories();

                for (int j = 0; j < dirs.Length; j++)
                {
                    CopyDirectory(dirs[j].FullName, Path.Combine(target.FullName, dirs[j].Name));
                }
            }

            public static void ReplaceExtensionName(string srcDir, string extensionName, string newExtensionName)
            {
                if (Directory.Exists(srcDir))
                {
                    string[] fls = Directory.GetFiles(srcDir);

                    foreach (string fl in fls)
                    {
                        if (fl.EndsWith(extensionName))
                        {
                            File.Move(fl, fl.Substring(0, fl.IndexOf(extensionName)) + newExtensionName);
                            File.Delete(fl);
                        }
                    }

                    string[] subDirs = Directory.GetDirectories(srcDir);

                    foreach (string subDir in subDirs)
                    {
                        ReplaceExtensionName(subDir, extensionName, newExtensionName);
                    }
                }
            }
        }

    }

}
