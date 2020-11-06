using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class LoadUnitySceneEvent : AEvent<LoadUnityScene>
    {
        public override async ETTask Run(LoadUnityScene args)
        {
            await SceneHelper.LoadSceneAsync(args.Id);
        }
    }
}
