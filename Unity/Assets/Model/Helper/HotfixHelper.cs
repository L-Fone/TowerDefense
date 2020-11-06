namespace ET
{
    public static class HotfixHelper
    {
        public static void StartHotfix()
        {
            MonoHelper.LoadHotfixAssembly().Coroutine();
//#if XLUA
//            LuaHelper.StartHotfix();
//#else
//#endif
        }
    }
}
