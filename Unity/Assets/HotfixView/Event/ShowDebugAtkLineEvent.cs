using ET.EventType;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class ShowDebugAtkLineEvent : AEvent_Sync<ShowDebugAtkLine>
    {
        public override void Run(ShowDebugAtkLine args)
        {
            var unit = args.unit;
            var target = args.target;
            var view = target.GetComponent<UnitView>();
            Debug.DrawLine(unit.GetComponent<UnitView>().Position, view.Position, Color.red,0.3f);
            Transform tran = view.transform.Find("Go");
            if (tran == null)
            {
                GameObject go = new GameObject("Go");
                tran = go.transform;
                tran.SetParent(view.transform);
                tran.localPosition = Vector3.zero;
            }
            var text = tran.gameObject.GetOrAddComponent<TextMesh>();
            text.fontSize = 12;
            text.text = $"{args.hp-args.damage}/{args.maxHp}";
            
        }
    }
}
