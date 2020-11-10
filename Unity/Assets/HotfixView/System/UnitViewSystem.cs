using Cal.DataTable;
using ET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ET
{
    public class UnitViewAwakeSystem : AwakeSystem<UnitView, GameObject>
    {
        public override void Awake(UnitView self, GameObject gameObject)
        {
            var unit = self.unit = self.GetParent<Unit>();
            self.gameObject = gameObject;
            self.transform = gameObject.transform;
            if(!self.spriteRenderer)
                self.spriteRenderer = gameObject.GetOrAddComponent<SpriteRenderer>();
            if (!self.spriteAnimator)
                self.spriteAnimator = gameObject.GetOrAddComponent<SpriteAnimator>();
            gameObject.GetOrAddComponent<ComponentView>().Component = self;

            var children = gameObject.GetComponentsInChildren<Transform>();
            foreach (var item in children)
            {
                if (item.name.Equals("HeadPoint"))
                {
                    self.HeadPoint = item;
                }
                else if (item.name.Equals("FootPoint"))
                {
                    self.FootPoint = item;
                }
            }
            if (self.FootPoint)
            {
                self.yDelta = self.transform.position.y - self.FootPoint.position.y;
                self.xDelta = self.transform.position.x - self.FootPoint.position.x;
            }
           
        }
    }
    public class UnitViewDestroySystem : DestroySystem<UnitView>
    {
        public override void Destroy(UnitView self)
        {
            ResourceViewHelper.DestoryPrefabAsync(self.gameObject);
            self.gameObject = null;
            self.transform = null;
            self.spriteAnimator = null;
            self.spriteRenderer = null;
            self.HeadPoint = null;
            self.FootPoint = null;
        }
    }

    public static class UnitViewSystem
    {

        public static async ETTask ChangeSkin(this UnitView self, int prefabId)
        {
            ResourceViewHelper.DestoryPrefabAsync(self.gameObject);
            self.gameObject = (await ResourceViewHelper.LoadPrefabAsync(prefabId)).gameObject;
        }
        public static void SetFootEffect(this UnitView self, UnitType unitType)
        {
            Color footEffectColor = Color.white;
            switch (unitType)
            {
                case UnitType.None:
                    break;
                case UnitType.Player:
                    footEffectColor = Color.white;
                    break;
                case UnitType.MainStoryMonster:
                case UnitType.TrialCopyMonster:
                case UnitType.BossMonster:
                case UnitType.Enermy:
                    footEffectColor = Color.red;
                    break;
                case UnitType.OtherPlayer:
                    footEffectColor = Color.yellow;
                    break;
                case UnitType.TeamMember:
                    footEffectColor = Color.yellow;
                    break;
                default:
                    break;
            }
        }
    }
}