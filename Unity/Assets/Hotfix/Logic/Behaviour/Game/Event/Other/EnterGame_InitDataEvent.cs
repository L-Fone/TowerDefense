using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class EnterGame_InitDataEvent : AEvent<EnterGame_InitData>
    {
        public override async ETTask Run(EnterGame_InitData args)
        {
            //加载初始化本地数据
            var scene = args.zoneScene;
            var user = UserFactory.Create(scene, IdGenerater.GenerateId());
            UserComponent.MyUser = user;
            user.UserName = "XXX";
            user.AddComponent<UserData>();

            Game.EventSystem.Publish(new ET.EventType.EnterGameFinish_CreateMainUI
            { 
               zoneScene =scene
            }).Coroutine();
            await ETTask.CompletedTask;
        }
    }
}
