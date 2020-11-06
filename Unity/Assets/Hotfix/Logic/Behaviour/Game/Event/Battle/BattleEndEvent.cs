using ET.EventType;
using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class BattleEndEvent : AEvent<BattleEnd>
    {
        public override async ETTask Run(BattleEnd args)
        {
            try
            {
                Game.EventSystem.Publish(new ET.EventType.TranslateSceneStart {isAutoEnd =false}).Coroutine();
                PosHelper.BackPos();
                Game.EventSystem.Publish(new ET.EventType.StateBuffUI_Init
                {

                }).Coroutine();
                Game.EventSystem.Publish(new ET.EventType.ShowOthersUnit
                { 
                   zoneScene = args.zoneScene
                }).Coroutine();
                BattleComponent.Clear();
                Game.EventSystem.Publish(new ET.EventType.TranslateSceneEnd { }).Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            await ETTask.CompletedTask;
        }
    }
}
