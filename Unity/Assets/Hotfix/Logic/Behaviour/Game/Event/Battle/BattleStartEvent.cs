using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class BattleStartEvent : AEvent<EventType.BattleStart>
    {
        public override async ETTask Run(BattleStart args)
        {
            try
            {
                Game.EventSystem.Publish(new ET.EventType.TranslateSceneStart
                {
                    isAutoEnd = false
                }).Coroutine();
                Game.EventSystem.Publish(new ET.EventType.StateBuffUI_Init
                {
                    zoneScene =args.zoneScene
                }).Coroutine();

                Game.EventSystem.Publish(new ET.EventType.HideOthersUnit
                { 
                   zoneScene = args.zoneScene
                }).Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

            await ETTask.CompletedTask;
        }
    }
}
