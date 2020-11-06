using System;
using System.Collections;
using System.Collections.Generic;
using ET;
using FairyGUI;
using UnityEngine;

namespace ET
{

    public class PopupComponentAwakeSystem : AwakeSystem<PopupComponent>
    {
        public override void Awake(PopupComponent self)
        {
            self.unit = self.GetParent<Unit>();
            self.unitView = self.unit.GetComponent<UnitView>();
        }
    }


    public class PopupComponentUpdateSystem : UpdateSystem<PopupComponent>
    {
        public override void Update(PopupComponent self)
        {
            self.Update();
        }
    }

    /// <summary>
    /// 飘字组件，用于处理诸如伤害飘字。
    /// </summary>
    public class PopupComponent : Entity
    {
        /// <summary>
        /// 预备中的飘字组件队列
        /// </summary>
        private Dictionary<Type, Queue<FUI>> PrePopupDamageDic = new Dictionary<Type, Queue<FUI>>();

        private Queue<(int, bool)> damangeQue = new Queue<(int, bool)>();

        /// <summary>
        /// 运行中的飘字组件队列
        /// </summary>
        private Dictionary<Type, Queue<FUI>> RunnningPopupDic = new Dictionary<Type, Queue<FUI>>();

        public Unit unit;
        public UnitView unitView;

        private int _popupCount;
        private long _lastTime;



        private long lastDamageTime;
        private bool hasDamageTime;
        /// <summary>
        /// 播放飘字特效
        /// </summary>
        /// <param name="targetValue">目标值</param>
        public void PlayDamage(int targetValue, bool isCrit)
        {
            //!更新UI
            if (unit.UnitType == UnitType.Player || unit.UnitType == UnitType.TeamMember)
                Game.EventSystem.Run(EventIdType.UpdateHeadInfo, unit.Id, 0.2f);
            damangeQue.Enqueue((targetValue, isCrit));
        }
        /// <summary>
        /// 播放获得奖励特效
        /// </summary>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public async ETVoid PlayReward(string icon, string title)
        {
            if ((TimeHelper.Now() - _lastTime) > 1000)
            {
                _popupCount = 0;
                _lastTime = TimeHelper.Now();
            }
            await TimerComponent.Instance.WaitAsync(600 * _popupCount++);

            if (!PrePopupDamageDic.TryGetValue(typeof(FUI_RewardUI), out var que))
            {
                que = PrePopupDamageDic[typeof(FUI_RewardUI)] = new Queue<FUI>();
            }
            if (que.Count == 0)
            {
                FUI_RewardUI hotfixui = FUI_RewardUI.CreateInstance(FUIComponent.Instance);
                hotfixui.Name = hotfixui.Id.ToString();
                FUIComponent.Instance.Add(hotfixui, true);
                que.Enqueue(hotfixui);
            }
            FUI_RewardUI fuiFallBleed = que.Dequeue() as FUI_RewardUI;

            fuiFallBleed.m_icon.icon = icon;
            fuiFallBleed.m_title.text = title;
            if (!RunnningPopupDic.TryGetValue(typeof(FUI_RewardUI), out var runningQue))
            {
                runningQue = RunnningPopupDic[typeof(FUI_RewardUI)] = new Queue<FUI>();
            }
            runningQue.Enqueue(fuiFallBleed);
            fuiFallBleed.m_Effect.Play(() => CompleteCallBack(fuiFallBleed));
            fuiFallBleed.self.visible = true;
        }

        private void CompleteCallBack<T>(T fui) where T : FUI
        {
            fui.GObject.visible = false;
            if (!PrePopupDamageDic.TryGetValue(typeof(T), out var que))
            {
                Log.Error($"{typeof(T)} que ==null");
            }
            que.Enqueue(fui);
            if (!RunnningPopupDic.TryGetValue(typeof(T), out var runningQue))
            {
                Log.Error($"{typeof(T)} runningQue ==null");
            }
            runningQue.Dequeue();
        }
        public void Update()
        {
            var now = TimeHelper.Now();
            if (damangeQue.Count > 0)
            {
                bool canShowDamage = false;
                if (!hasDamageTime || now - lastDamageTime >= 300)
                    canShowDamage = true;
                if (!canShowDamage)
                    return;
                hasDamageTime = true;
                lastDamageTime = now;
                (int targetValue, bool isCrit) = damangeQue.Dequeue();
                ShowDamage(targetValue,isCrit);
                
            }
            else
            {
                hasDamageTime = false;
            }
            foreach (var que in this.RunnningPopupDic.Values)
            {
                foreach (var item in que)
                {
                    // FGUI全局坐标转头顶血条本地坐标
                    item.GObject.position = PosHelper.WorldToScreen(unitView.HeadPoint.position);
                }
            }
        }

        private void ShowDamage(int targetValue, bool isCrit)
        {
            if (!this.PrePopupDamageDic.TryGetValue(typeof(FUI_Damage), out var que))
            {
                que = PrePopupDamageDic[typeof(FUI_Damage)] = new Queue<FUI>();
            }
            if (que.Count == 0)
            {
                FUI_Damage hotfixui = FUI_Damage.CreateInstance(FUIComponent.Instance);
                hotfixui.Name = hotfixui.Id.ToString();
                FUIComponent.Instance.Add(hotfixui, true);
                que.Enqueue(hotfixui);
            }
            FUI_Damage fuiFallBleed = que.Dequeue() as FUI_Damage;
            bool isTreat = targetValue > 0;

            //!颜色
            if (isCrit)
            {
                if (isTreat)
                    fuiFallBleed.m_txtCrit.color = Color.green;


                else
                    fuiFallBleed.m_txtCrit.color = Color.white;
                fuiFallBleed.m_crit.selectedIndex = 1;
                fuiFallBleed.m_txtCrit.text = targetValue.ToString();
            }
            else
            {
                if (isTreat)
                    fuiFallBleed.m_txt.color = Color.green;
                else
                    fuiFallBleed.m_txt.color = Color.red;

                fuiFallBleed.m_crit.selectedIndex = 0;
                fuiFallBleed.m_txt.text = targetValue.ToString();
            }
            if (!RunnningPopupDic.TryGetValue(typeof(FUI_Damage), out var runningQue))
            {
                runningQue = RunnningPopupDic[typeof(FUI_Damage)] = new Queue<FUI>();
            }
            runningQue.Enqueue(fuiFallBleed);
            var effect = isCrit ? fuiFallBleed.m_CritEffect : fuiFallBleed.m_Effect;
            effect.Play(() => CompleteCallBack(fuiFallBleed));
            fuiFallBleed.self.visible = true;
        }
    }
}