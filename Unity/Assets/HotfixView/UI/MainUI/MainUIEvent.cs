using System;
using System.Collections.Generic;

namespace ET
{
    [UIEvent(FUIPackage.Common_MainUI)]
    public class MainUIEvent : AUIEvent
    {
        public override async ETTask<FUI> OnCreate(FUIComponent fuiComponent)
        {
            if(!(fuiComponent.Get(FUIPackage.Common_MainUI)is FUI_MainUI ui))
            {
                ui =await FUI_MainUI.CreateInstanceAsync(fuiComponent.ZoneScene());
                ui.Name = FUIPackage.Common_MainUI;
                ui.MakeFullScreen();
            }
            return ui;
        }

        public override void OnRemove(FUIComponent fuiComponent)
        {

        }
    }
}
