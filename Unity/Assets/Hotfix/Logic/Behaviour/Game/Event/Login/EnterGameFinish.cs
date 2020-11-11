using ET;
using UnityEngine;

namespace ET
{
    public class EnterGameFinish : AEvent<ET.EventType.EnterGameFinish_SetCharacter>
    {
        public override async ETTask Run(ET.EventType.EnterGameFinish_SetCharacter args)
        {
            bool isOnline = args.isOnline;
            //!请求基础数据
            if (isOnline)
                SessionComponent.Instance.Session.Send(new C2M_GetStateReback { });
            else
                SessionComponent.Instance.Session.Send(new C2M_GetBasicalInfo { });
            await TimerComponent.Instance.WaitAsync(1500);
            var unit = UnitComponent.MyUnit;

            UnitCharacter clientUnit = UnitCharacterComponent.Instance.Get(unit.Id);
           

            Game.EventSystem.Publish(new ET.EventType.TranslateSceneEnd{}).Coroutine();
        }
    }
}