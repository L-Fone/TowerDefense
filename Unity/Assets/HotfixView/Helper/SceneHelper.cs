using Cal.DataTable;

namespace ET
{
    public static class SceneHelper
    {
        public static async ETTask LoadSceneAsync(int mapId, bool isAddtion = false)
        {
            string name = ConfigHelper.Get<MapSceneConfig>(mapId).Name;
            await LoadSceneAsync(name, isAddtion);
            GlobalVariable.MapId = mapId;
        }
        private static async ETTask LoadSceneAsync(string name, bool isAddtion = false)
        {
            Game.EventSystem.Publish(new ET.EventType.TranslateSceneStart
            {
                isAutoEnd = true
            }).Coroutine();
            await ResourceHelper.LoadSceneAsync(name, isAddtion);
            Game.EventSystem.Publish(new ET.EventType.TranslateSceneEnd
            {

            }).Coroutine();
        }
    }
}