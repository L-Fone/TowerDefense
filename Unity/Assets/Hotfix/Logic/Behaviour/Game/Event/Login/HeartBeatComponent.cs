

using ET;
using System;

namespace ET
{
    
    public class HeartBeatSystem : UpdateSystem<HeartBeatComponent>
    {
        public override void Update(HeartBeatComponent self)
        {
            self.Update();
        }
    }

    /// <summary>
    /// Session心跳组件(需要挂载到Session上)
    /// </summary>
    public class HeartBeatComponent : Entity
    {
        /// <summary>
        /// 心跳包间隔
        /// </summary>
        private const int SendInterval = 3*1000;

        /// <summary>
        /// 记录时间
        /// </summary>
        private long RecordDeltaTime = 0;

        /// <summary>
        /// 判断是否已经离线
        /// </summary>
        private bool hasOffline;

        public void Update()
        {
#if UNITY_EDITOR
            return;
#endif
            UpdateAsync().Coroutine();
        }

        private async ETVoid UpdateAsync()
        {
            if (this.hasOffline) return;

            // 如果还没有建立Session直接返回、或者没有到达发包时间
            var now = TimeHelper.ClientNow();
            if (now - this.RecordDeltaTime < SendInterval) return;
            // 记录当前时间
            this.RecordDeltaTime = now;

            // 开始发包
            try
            {
                G2C_HeartBeat ret = (G2C_HeartBeat)await this.GetParent<Session>().Call(new C2G_HeartBeat());
                if (!ret.Message.IsNullOrEmpty())
                {
                    Game.EventSystem.Publish(new ET.EventType.ShowTipUI
                    { 
                       tip =ret.Message
                    }).Coroutine();
                    return;
                }
                Log.Info($"FPS: {1000f / ret.DelayTime:f1}   time: {ret.DelayTime:f2}");
            }
            catch
            {
                if (this.hasOffline) return;
                this.hasOffline = true;
                Log.Error("发送心跳包失败");
                //!+ 执行相关操作
                await Game.EventSystem.Publish(new ET.EventType.Quit());
            }
        }
    }
}
