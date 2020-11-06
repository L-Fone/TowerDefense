using Cal.DataTable;
using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class InitSceneEndEvent : AEvent<InitSceneEnd>
    {
        public override async ETTask Run(InitSceneEnd args)
        {
            var scene = args.zoneScene;
            await SceneHelper.LoadSceneAsync(MapSceneConfigId.Scene_EnterGame);
            Game.EventSystem.Publish(new ET.EventType.EnterGame_Open
            { 
               zoneScene =scene
            }).Coroutine();
            await ETTask.CompletedTask;
        }
    }
}
