using System;
using ET;


namespace ET
{
    public static class Init
    {
        public static async void Start()
        {
            try
            {
                Log.Info($"进入热更");
                HotfixMongoHelper.Init();
                Scene zoneScene = Game.Hotfix.zoneScene;
                string clientVersion = await GlobalHotfixProtoHelper.GetClintVersion();
                if (!clientVersion.Equals(GlobalConfigComponent.Instance.GlobalProto.ClientVersion))
                {
                    Game.EventSystem.Publish(new ET.EventType.ShowMessageBox
                    {
                        title = "提示！",
                        content = $"客户端有更新，请下载最新版客户端{clientVersion}！",
                        action = async (MessageBoxEventId id) =>
                        {
                            await Game.EventSystem.Publish(new ET.EventType.Quit());
                        }
                    }).Coroutine();
                }

                if (Define.hasView)
                {
                    Game.EventSystem.Add(Game.Hotfix.hotfix);
                    Game.EventSystem.Add(Game.Hotfix.hotfixView);
                }
                else
                {
                    Game.EventSystem.Add(Game.Hotfix.hotfix);
                }


                Game.Scene.GetOrAddComponent<MessageDispatcherComponent>();

                Game.Scene.GetOrAddComponent<ConfigComponent>();
                Game.Scene.GetOrAddComponent<NumericWatcherComponent>();


                zoneScene.AddComponent<UserComponent>();
                zoneScene.AddComponent<UnitComponent>();
                zoneScene.AddComponent<BattleMgrComponent>();
                zoneScene.AddComponent<WordComponent>();
                zoneScene.AddComponent<UnitCharacterComponent>();
                zoneScene.AddComponent<HideUnitComponent>();
                zoneScene.AddComponent<TowerPointComponent>();

                Game.EventSystem.Publish(new ET.EventType.AfterCreateZoneScene() { zoneScene = zoneScene }).Coroutine();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}