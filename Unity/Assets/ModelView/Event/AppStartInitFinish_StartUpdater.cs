using ET.EventType;
using libx;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class AppStartInitFinish_StartUpdater : AEvent<AppStartInitFinish>
    {
        public override async ETTask Run(AppStartInitFinish args)
        {
            UpdateScreen updater = GameObject.Find("Updater/UpdateScreen").GetComponent<UpdateScreen>();
            updater.enabled = true;

            await ETTask.CompletedTask;
        }
    }
}
