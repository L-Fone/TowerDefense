using ET.EventType;
using ET;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cal.DataTable;

namespace ET
{
    public class OnCreateUnitEvent : AEvent<ET.EventType.OnCreateUnit>
    {
        public override async ETTask Run(OnCreateUnit args)
        {
            var unit = args.unit;

            GameObject go = (await ResourceViewHelper.LoadPrefabAsync(args.prefabId)).gameObject;
            unit.AddComponent<UnitView, GameObject>(go);
            unit.AddComponent<PopupComponent>();
            var animation = go.GetOrAddComponent<SpriteAnimator>();

            RoleConfig roleConfig = ConfigHelper.Get<RoleConfig>(unit.ConfigId);
            animation.texture2D =await ResourceHelper.LoadAssetAsync<Texture2D>(roleConfig.AsltasPath);
            animation.framesPerSecond = roleConfig.Frame;

            await ETTask.CompletedTask;
        }
    }
}
