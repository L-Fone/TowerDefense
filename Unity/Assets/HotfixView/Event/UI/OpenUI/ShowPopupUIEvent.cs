using ET.EventType;
using FairyGUI;
using System;
using System.Collections.Generic;

namespace ET
{
    public class ShowPopupUIEvent : AEvent_Sync<ShowPopupUI>
    {
        private PopupMenu menu;
        public override void Run(ShowPopupUI args)
        {
            //显示菜单项
            if (menu == null)
            {
                menu = new PopupMenu(FUI_PvpPopupMenu.URL);
                foreach (var info in args.popupMenuInfo)
                {
                    var action = info.action;
                    menu.AddItem(info.name, context =>
                    {
                        action?.Invoke();
                    });
                }
            }
            menu.Show();
        }
    }

}
