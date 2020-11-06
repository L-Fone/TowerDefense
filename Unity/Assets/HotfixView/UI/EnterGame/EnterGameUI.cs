using System;
using System.Collections.Generic;

namespace ET
{
    public class EnterGameUIAwakeSystem : AwakeSystem<EnterGameUI>
    {
        public override void Awake(EnterGameUI self)
        {
            self.Awake();
        }
    }

    public class EnterGameUI : Entity
    {
        private FUI_EnterGame ui;
        internal void Awake()
        {
            this.ui = this.GetParent<FUI_EnterGame>();
            InternelAwake();
        }
        private void InternelAwake()
        {
            ui.m_btnEnterGame.onClick.Set(() =>
            {
                Log.Info($"开始游戏");
                Game.EventSystem.Publish(new ET.EventType.EnterGame_InitData
                { 
                   zoneScene =ui.ZoneScene()
                }).Coroutine();
                FUIComponent.Instance.Remove(ui.Name);

            });
        }
    }
}
