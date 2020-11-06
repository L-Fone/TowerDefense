using ET.EventType;
using libx;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class AppStartEvent : AEvent<AppStart>
    {
        public override async ETTask Run(AppStart args)
        {
            //!设置帧率
            Application.targetFrameRate = Define.FrameRate;

            Game.Scene.AddComponent<TimerComponent>();
            Game.Scene.AddComponent<GlobalConfigComponent>();

            Define.IsEditorMode = GlobalConfigComponent.Instance.GlobalProto.isEditorMode;

            var keyComponent = Game.Scene.AddComponent<GameKeyComponent>();
            keyComponent.key = args.key;
            keyComponent.keyIV = args.keyIV;
            keyComponent.xorKey = args.xorKey;


            Game.Scene.AddComponent<OpcodeTypeComponent>();
            Game.Scene.AddComponent<MessageDispatcherComponent>();
            Game.Scene.AddComponent<CoroutineLockComponent>();
            Game.Scene.AddComponentNoPool<Updater>();

            Scene zoneScene = SceneFactory.CreateZoneScene(1, 1, "Game");

            await Game.EventSystem.Publish(new EventType.AppStartInitFinish() { zoneScene = zoneScene });

            await ETTask.CompletedTask;
        }
    }
}
