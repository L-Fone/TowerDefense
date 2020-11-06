using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ET
{
    namespace EventType
    {
        public struct AppStart
        {
            public string key;
            public string keyIV;
            public byte[] xorKey;
        }

        public struct AppStartInitFinish
        {
            public Scene zoneScene;
        }
        public struct DownloadInitResourceFinish
        {
            public Scene zoneScene;
        }
        public struct ShowMessageBox
        {
            public string title;
            public string content;
            public string ok;
            public string no;
            public Action<MessageBoxEventId> action;
        }
        public struct MessageBox_CloseAll
        {

        }
        public struct MessageBox_Dispose
        {

        }
        public struct OnEnterSceneTranPoint
        {
            public Scene zoneScene;
            public Collider2D collision;
            public long Id;
        }
    }
}
