//using ET.EventType;
//using System;
//using System.Collections.Generic;
//using UnityEngine;

//namespace ET
//{
//    public class UnitMoveEvent : AEvent<UnitMove>
//    {
//        public override async ETTask Run(UnitMove args)
//        {
//            var unit = args.unit;
//            var unitView = unit.GetComponent<UnitView>();
//            unitView.transform.Translate(args.dir * args.spd * Time.deltaTime);
//            await ETTask.CompletedTask;
//        }
//    }
//}
