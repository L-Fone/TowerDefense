
using Cal.DataTable;
using ET.EventType;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class GenerateTowerPointEvent : AEvent<GenerateTowerPoint>
    {
        public override async ETTask Run(GenerateTowerPoint args)
        {
            var tran = await ResourceViewHelper.LoadPrefabAsync(PrefabConfigId.TowerPointTrigger);
            UnitView unitView = args.towerPointInfo.AddComponent<UnitView,GameObject>(tran.gameObject);
            unitView.Position = args.point;
            unitView.gameObject.GetOrAddComponent<TowerPointMono>().Id = args.towerPointInfo.Id;

            await ETTask.CompletedTask;
        }
    }
}
