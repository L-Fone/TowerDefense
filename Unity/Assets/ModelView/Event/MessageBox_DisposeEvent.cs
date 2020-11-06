using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class MessageBox_DisposeEvent : AEvent<MessageBox_Dispose>
    {
        public override async ETTask Run(MessageBox_Dispose args)
        {
            MessageBox.Dispose();
            await ETTask.CompletedTask;
        }
    }
}
