using System;
using System.Collections.Generic;

namespace ET
{
    [UIEvent(FUIPackage.Login_EnterGame)]
    public class EnterGameUIEvent : AUIEvent
    {
        public override async ETTask<FUI> OnCreate(FUIComponent fuiComponent)
        {
            if(!(fuiComponent.Get(FUIPackage.Login_EnterGame)is FUI_EnterGame ui))
            {
                ui =await FUI_EnterGame.CreateInstanceAsync(fuiComponent.ZoneScene());
                ui.Name = FUIPackage.Login_EnterGame;
                ui.MakeFullScreen();
            }
            return ui;
        }

        public override void OnRemove(FUIComponent fuiComponent)
        {

        }
    }
}
