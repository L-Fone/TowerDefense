using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{

    public class EnterGameFinish_CreateMainUIEvent : AEvent<ET.EventType.EnterGameFinish_CreateMainUI>
    {
        public override async ETTask Run(EnterGameFinish_CreateMainUI args)
        {
            var ui = await FUIHelper.Open<FUI_MainUI>(args.zoneScene, FUIPackage.Common_MainUI, WindowPos.Center, (_ui) =>
            {

            });
            ui.AddComponent<MainUI>();

        }
    }
}
