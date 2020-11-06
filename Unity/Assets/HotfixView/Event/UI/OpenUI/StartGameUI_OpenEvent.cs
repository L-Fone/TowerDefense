using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{

    public class StartGameUI_OpenEvent : AEvent<ET.EventType.EnterGame_Open>
    {
        public override async ETTask Run(EnterGame_Open args)
        {
            var ui = await FUIHelper.Open<FUI_EnterGame>(args.zoneScene, FUIPackage.Login_EnterGame, WindowPos.Center, (_ui) =>
            {

            });
            ui.AddComponent<EnterGameUI>();
            ui.GetComponent<FUIWindowComponent>().Window.OnHideEvent += () =>
             {
                 ui.RemoveComponent<EnterGameUI>();
             };
        }
    }
}
