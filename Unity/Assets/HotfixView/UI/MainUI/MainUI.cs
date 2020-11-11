using Cal.DataTable;
using System;
using System.Collections.Generic;

namespace ET
{
    public class MainUIAwakeSystem : AwakeSystem<MainUI>
    {
        public override void Awake(MainUI self)
        {
            self.Awake();
        }
    }

    public class MainUI : Entity
    {
        private FUI_MainUI ui;
        internal void Awake()
        {
            this.ui = this.GetParent<FUI_MainUI>();
            InternelAwake();
        }
        private void InternelAwake()
        {
            ui.m_btnLittleGame.onClick.Set(()=> { StartNormalBattle().Coroutine(); });
            ui.m_btnChatReceive.onClick.Set(SwitchChatUI);
        }

        private void SwitchChatUI()
        {
            ui.m_chatBoard.visible = !ui.m_chatBoard.visible;
        }

        private async ETVoid StartNormalBattle()
        {
            var battle = BattleMgrComponent.Create<NormalBattle>();
            await SceneHelper.LoadSceneAsync(MapSceneConfigId.Scene_Level0);
            battle.Init();
        }
    }
}
