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
            self.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            self.monoAnimancer = gameObject.GetComponent<MonoAnimancer>();
            if (self.monoAnimancer && self.monoAnimancer.Animator == null)
                self.monoAnimancer.Animator = gameObject.GetComponentInChildren<Animator>();
            gameObject.GetOrAddComponent<ComponentView>().Component = self;

            var children = gameObject.GetComponentsInChildren<Transform>();
            foreach (var item in children)
            {
                if (item.name.Equals("HeadBarPoint"))
                {
                    self.HeadPoint = item;
                }
                else if (item.name.Equals("FootPoint"))
                {
                    self.FootPoint = item;
                }
                else if (item.name.Equals("Select"))
                {
                    self.footEffect = item.GetComponent<SpriteRenderer>();
                }
            }
            if (self.FootPoint)
            {
                self.yDelta = self.transform.position.y - self.FootPoint.position.y;
                self.xDelta = self.transform.position.x - self.FootPoint.position.x;
            }
            if (self.footEffect)
                SetColor().Coroutine();
            async ETVoid SetColor()
            {
                Color footEffectColor = Color.white;
                if (self.FootPoint)
                {
                    var footEffctTran = await ResourceViewHelper.LoadPrefabAsync(PrefabId.Select);
                    footEffctTran.name = "Select";
                    footEffctTran.SetParent(self.transform);
                    footEffctTran.position = self.FootPoint.position;
                    self.footEffect = footEffctTran.GetComponent<SpriteRenderer>();
                    self.footEffect.sortingLayerName = "FootEffct";
                }
                self.SetFootEffcetActive(true, footEffectColor);
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
            self.spriteRenderer = null;
            self.monoAnimancer = null;
            self.HeadPoint = null;
            self.FootPoint = null;
            self.footEffect = null;
        }
    }

    public static class UnitViewSystem
    {

        public static void SetFootEffcetActive(this UnitView self, bool isActive, Color color)
        {
            if (!self.footEffect) return;
            self.footEffect.gameObject.SetActive(isActive);
            self.footEffect.color = color;
        }
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
            self.SetFootEffcetActive(true, footEffectColor);
        }
    }
}