
using ET;

namespace ET
{
    public abstract class AUIEvent
    {
        public abstract ETTask<FUI> OnCreate(FUIComponent fuiComponent);
        public abstract void OnRemove(FUIComponent fuiComponent);
    }
}