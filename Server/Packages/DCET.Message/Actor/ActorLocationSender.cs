using System;
using System.Threading.Tasks;

namespace DCET
{
    [ObjectSystem]
    public class ActorLocationSenderAwakeSystem : AwakeSystem<ActorLocationSender>
    {
        public override void Awake(ActorLocationSender self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class ActorLocationSenderDestroySystem : DestroySystem<ActorLocationSender>
    {
        public override void Destroy(ActorLocationSender self)
        {
            self.Destroy();
        }
    }

    // 知道对方的Id，使用这个类发actor消息
    public class ActorLocationSender : Entity
    {
        public long ActorId;

        // 最近接收或者发送消息的时间
        public long LastSendOrRecvTime;
		
        public int FailTimes;

        public const int MaxFailTimes = 5;

        public void Awake()
        {
            LastSendOrRecvTime = TimeHelper.CurrentLocalMilliseconds();
            FailTimes = 0;
            ActorId = 0;

            StartAsync();
        }

        public void Destroy()
        {
            Log.Debug($"actor location remove: {Id}");
            LastSendOrRecvTime = 0;
            ActorId = 0;
            FailTimes = 0;
        }

        private async Task<IActorResponse> Run(IActorRequest iActorRequest)
        {
            long instanceId = InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ActorLocationSender, Id))
            {
                if (InstanceId != instanceId)
                {
                    throw new RpcException(ErrorCode.ERR_ActorRemove, $"{DCETRuntime.MongoHelper.ToJson(iActorRequest)}");
                }

                return await RunInner(iActorRequest);
            }
        }

        private async Task<IActorResponse> RunInner(IActorRequest iActorRequest)
        {
            try
            {
                if (ActorId == 0)
                {
                    Log.Info($"actor send message fail, actorid: {Id}");
                    Dispose();
                    return new ActorResponse() { Error = ErrorCode.ERR_ActorNotOnline };
                }

                LastSendOrRecvTime = TimeHelper.CurrentLocalMilliseconds();
                IActorResponse response = await ActorMessageSenderComponent.Instance.CallWithoutException(ActorId, iActorRequest);

                switch (response.Error)
                {
                    case ErrorCode.ERR_NotFoundActor:
                    {
                        // 如果没找到Actor,重试
                        ++FailTimes;
                        if (FailTimes > ActorLocationSender.MaxFailTimes)
                        {
                            Log.Info($"actor send message fail, actorid: {Id}");
                            Dispose();
                            return response;
                        }

                        // 等待0.5s再发送
                        long instanceId = InstanceId;
                        await TimerComponent.Instance.WaitAsync(500);
                        if (InstanceId != instanceId)
                        {
                            throw new RpcException(ErrorCode.ERR_ActorRemove, $"{DCETRuntime.MongoHelper.ToJson(iActorRequest)}");
                        }
                        ActorId = await LocationProxyComponent.Instance.Get(Id);
                        return await RunInner(iActorRequest);
                    }
                }

                LastSendOrRecvTime = TimeHelper.CurrentLocalMilliseconds();
                FailTimes = 0;

                return response;
            }
            catch (RpcException)
            {
                Dispose();
                throw;
            }
            catch (Exception e)
            {
                Dispose();
                throw new Exception($"{DCETRuntime.MongoHelper.ToJson(iActorRequest)}\n{e}");
            }
        }

        public async void Send(IActorLocationMessage request)
        {
            if (request == null)
            {
                throw new Exception($"actor location send message is null");
            }

            await Run(request);
        }

        public async Task<IActorResponse> Call(IActorLocationRequest request)
        {
            if (request == null)
            {
                throw new Exception($"actor location call message is null");
            }

            return await Run(request);
        }

        public async void StartAsync()
        {
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.ActorLocationSender, Id))
            {
                ActorId = await Game.Scene.GetComponent<LocationProxyComponent>().Get(Id);
            }
        }
    }
}