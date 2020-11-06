using ET;
using FairyGUI;
using System;
using System.Collections.Generic;

namespace ET
{
    public enum WindowPos
    {
        None,
        Center,
        Left,
        Right,
    }
    public static class FUIHelper
    {
        public static GWindow asGWindow(this GObject gObject)
        {
            return gObject as GWindow;
        }
        public static async ETTask<FUI> Create(Scene scene, string uiType)
        {
            return await scene.GetComponent<FUIComponent>().Create(uiType);
        }
        public static async ETTask<FUI> Open(Scene scene, string uiType,WindowPos windowPos,Action<FUI> effcetAction = null)
        {
            var fui =await Create(scene, uiType);
            var window = fui.GetOrAddComponent<FUIWindowComponent>();
            if (window.IsShowing)
            {
                window.Hide();
                return fui;
            }
            window.Show();
            effcetAction?.Invoke(fui);
            switch (windowPos)
            {
                default:
                case WindowPos.None:
                    break;
                case WindowPos.Center:
                    window.Window.Center();
                    break;
                case WindowPos.Left:
                    window.Window.Left();
                    break;
                case WindowPos.Right:
                    window.Window.Right();
                    break;
            }
            return fui;
        }
        public static async ETTask<T> Open<T>(Scene scene, string uiType, WindowPos windowPos, Action<T> effcetAction = null)where T:FUI
        {
            var fui = await Create(scene, uiType);
            T t = fui.As<T>();
            var window = fui.GetOrAddComponent<FUIWindowComponent>();
            if (window.IsShowing)
            {
                window.Hide();
                return t;
            }
            window.Show();
            effcetAction?.Invoke(t);
            switch (windowPos)
            {
                case WindowPos.Center:
                    window.Window.Center();
                    break;
                case WindowPos.Left:
                    window.Window.Left();
                    break;
                case WindowPos.Right:
                    window.Window.Right();
                    break;
                default:
                    break;
            }
            return t;
        }

        public static void Remove(Scene scene, string uiType)
        {
            scene.GetComponent<FUIComponent>().Remove(uiType);
        }
    }
}
