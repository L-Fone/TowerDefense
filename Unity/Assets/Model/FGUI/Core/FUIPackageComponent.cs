using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 管理所有UI Package
    /// </summary>
    public class FUIPackageComponent: Entity
    {
        public const string FUI_PACKAGE_DIR = "Assets/Download/FGUI";

        private readonly HashSet<UIPackage> packages = new HashSet<UIPackage>();

        private bool IsLoadedFUIAB;

        public void AddPackageAsync(string uitype)
        {
            UIPackage uiPackage;
            if (Define.IsEditorMode)
            {
                uiPackage = UIPackage.AddPackage($"{FUI_PACKAGE_DIR}/{uitype}/{uitype}");
            }
            else
            {
                //AssetBundle ab = await ResourceHelper.LoadAssetBundleAsync(string.Format(PathHelper.FUIABFormat, uitype), true);

                UIPackage.AddPackage($"{FUI_PACKAGE_DIR}/{uitype}/{uitype}",
                    (string name, string extension, System.Type type, out DestroyMethod destroyMetho) =>
                    {
                        destroyMetho = DestroyMethod.Unload;
                        switch (extension)
                        {
                            case ".bytes":
                                {
                                    var req = ResourceHelper.LoadAsset<TextAsset>($"{name}{extension}");
                                    return req;
                                }
                            case ".png":
                                {
                                    var req = ResourceHelper.LoadAsset<Texture>($"{name}{extension}");
                                    return req;
                                }
                        }

                        //return ResourceHelper.LoadAsset(name + extension, type);
                        return null;
                    });
            }
        }

        public void RemovePackage(string type)
        {
            UIPackage package;

            //if (packages.TryGetValue(type, out package))
            //{
            //    var p = UIPackage.GetByName(package.name);

            //    if (p != null)
            //    {
            //        UIPackage.RemovePackage(package.name);
            //    }

            //    packages.Remove(package.name);
            //}

            if (!Define.IsEditorMode)
            {

            }
        }
    }
}