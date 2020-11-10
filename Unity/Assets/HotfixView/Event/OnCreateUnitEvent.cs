using ET.EventType;
using ET;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cal.DataTable;

namespace ET
{
    public class OnCreateUnitEvent : AEvent_Sync<ET.EventType.OnCreateUnit>
    {
        public override void Run(OnCreateUnit args)
        {
            RunAsync(args).Coroutine();
        }
        async ETVoid RunAsync(OnCreateUnit args)
        {
            var unit = args.unit;
            RoleConfig roleConfig = args.roleConfig;

            GameObject go = (await ResourceViewHelper.LoadPrefabAsync(roleConfig.PrefabId)).gameObject;
            unit.AddComponent<UnitView, GameObject>(go);
            unit.AddComponent<PopupComponent>();
            var animation = go.GetOrAddComponent<SpriteAnimator>();

            animation.texture2D = await ResourceHelper.LoadAssetAsync<Texture2D>(roleConfig.AsltasPath);
            animation.framesPerSecond = roleConfig.Frame;

            if (unit.UnitType == UnitType.Tower)
            {
                CircleCollider2D collider = go.GetOrAddComponent<CircleCollider2D>();
                collider.radius = roleConfig.AtkField;
                collider.isTrigger = true;
#if UNITY_EDITOR
                go.GetOrAddComponent<AtkFieldDebugMono>().atkField = roleConfig.AtkField;
#endif
            }

        }
    }
}
