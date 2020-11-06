using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace ET
{
    public static  class FUIHelper
    {
        public static GComponent Add3DUI(this GameObject self,string pkgName,string cName,Camera camera,int sortingOrder)
        {
            //UIPanel的生命周期将和yourGameObject保持一致。再次提醒，注意yourGameObject的layer。
            UIPanel panel = self.AddComponent<UIPanel>();
            panel.packageName = pkgName;
            panel.componentName = cName;

            //下面这是设置选项非必须，注意很多属性都要在container上设置，而不是UIPanel

            //设置renderMode的方式
            panel.container.renderMode = RenderMode.WorldSpace;

            //设置renderCamera的方式
            panel.container.renderCamera =camera;

            //设置fairyBatching的方式
            panel.container.fairyBatching = true;

            //设置sortingOrder的方式
            panel.SetSortingOrder(sortingOrder, true);

            //设置hitTestMode的方式
            panel.SetHitTestMode(HitTestMode.Default);

            //最后，创建出UI
            panel.CreateUI();

            return panel.ui;
        }
        public static void Left(this GObject self)
        {
            GComponent r;
            if (self.parent != null)
                r = self.parent;
            else
                r = self.root;

            self.SetXY(0, (int)((r.height - self.height) / 2), true);
        }
        public static void Right(this GObject self)
        {
            GComponent r;
            if (self.parent != null)
                r = self.parent;
            else
                r = self.root;

            self.SetXY(r.width - self.width, (int)((r.height - self.height) / 2), true);
        }
    }
}
