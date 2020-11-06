using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class CloseAllMessageBoxEvent : AEvent<MessageBox_CloseAll>
    {
        public override async ETTask Run(MessageBox_CloseAll args)
        {
            MessageBox.CloseAll();
            await ETTask.CompletedTask;
        }
    }
}
