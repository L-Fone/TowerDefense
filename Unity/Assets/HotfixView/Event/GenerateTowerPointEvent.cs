
using Cal.DataTable;
using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class GenerateTowerPointEvent : AEvent<GenerateTowerPoint>
    {
        public override async ETTask Run(GenerateTowerPoint args)
        {
            var tran = await ResourceViewHelper.LoadPrefabAsync(PrefabConfigId.TowerPointTrigger);
            tran.position = args.point;
            tran.gameObject.AddComponent<TowerPointMono>().Id = args.Id;

            await ETTask.CompletedTask;
        }
    }
}
