using ET.EventType;
using System;
using System.Collections.Generic;

namespace ET
{
    public class ShowMessageBoxEvent : AEvent<ShowMessageBox>
    {
        public override async ETTask Run(ShowMessageBox args)
        {
            string ok = args.ok == null ? "确定" :args.ok;
            string no = args.no == null ? "取消" :args.no;
            var box = MessageBox.Show(args.title, args.content, ok, no);
            box.onComplete += args.action;
            await ETTask.CompletedTask;
        }
    }
}
