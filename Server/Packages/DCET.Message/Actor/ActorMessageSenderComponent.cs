using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCET
{
    [ObjectSystem]
    public class ActorMessageSenderComponentAwakeSystem : AwakeSystem<ActorMessageSenderComponent>
    {
        public override void Awake(ActorMessageSenderComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class ActorMessageSenderComponentDestroySystem : DestroySystem<ActorMessageSenderComponent>
    {
        public override void Destroy(ActorMessageSenderComponent self)
        {
            self.Destroy();
        }
    }

    public class ActorMessageSenderComponent: Entity
	{
		public static long TIMEOUT_TIME = 30 * 1000;
		
		public static ActorMessageSenderComponent Instance { get; set; }
		
		public int RpcId;
		
		public readonly Dictionary<int, ActorMessageSender> requestCallback = new Dictionary<int, ActorMessageSender>();

		public long TimeoutCheckTimer;
		
		public List<int> TimeoutActorMessageSenders = new List<int>();

        public void Awake()
        {
            TimeoutCheckTimer = TimerComponent.Instance.NewRepeatedTimer(10 * 1000, Check);
            Instance = this;
        }

        public void Destroy()
        {
            TimerComponent.Instance.Remove(TimeoutCheckTimer);
            TimeoutCheckTimer = 0;
            TimeoutActorMessageSenders.Clear();
            Instance = null;
        }


        public void Check()
        {
            long timeNow = TimeHelper.CurrentLocalMilliseconds();
            foreach ((int key, ActorMessageSender value) in requestCallback)
            {
                if (timeNow < value.CreateTime + TIMEOUT_TIME)
                {
                    continue;
                }
                TimeoutActorMessageSenders.Add(key);
            }

            foreach (int rpcId in TimeoutActorMessageSenders)
            {
                ActorMessageSender actorMessageSender = requestCallback[rpcId];
                requestCallback.Remove(rpcId);
                Log.Error($"actor request timeout: {rpcId}");
                actorMessageSender.Callback.Invoke(new ActorResponse() { Error = ErrorCode.ERR_ActorTimeout });
            }

            TimeoutActorMessageSenders.Clear();
        }

        public void Send(long actorId, IActorMessage message)
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {DCETRuntime.MongoHelper.ToJson(message)}");
            }
            string address = StartConfigComponent.Instance.GetProcessInnerAddress(IdGenerater.GetProcessId(actorId));
            Session session = NetInnerComponent.Instance.Get(address);
            message.ActorId = actorId;
            session.Send(message);
        }

        public Task<IActorResponse> Call(long actorId, IActorRequest message)
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {DCETRuntime.MongoHelper.ToJson(message)}");
            }

            string address = StartConfigComponent.Instance.GetProcessInnerAddress(IdGenerater.GetProcessId(actorId));
            Session session = NetInnerComponent.Instance.Get(address);
            message.ActorId = actorId & IdGenerater.HeadMask | IdGenerater.Head;
            message.RpcId = ++RpcId;

            var tcs = new TaskCompletionSource<IActorResponse>();
            requestCallback.Add(message.RpcId, new ActorMessageSender((response) =>
            {
                if (ErrorCode.IsRpcNeedThrowException(response.Error))
                {
                    tcs.SetException(new Exception($"Rpc error: {DCETRuntime.MongoHelper.ToJson(response)}"));
                    return;
                }


                tcs.SetResult(response);
            }));
            session.Send(message);
            return tcs.Task;
        }

        public Task<IActorResponse> CallWithoutException(long actorId, IActorRequest message)
        {
            if (actorId == 0)
            {
                throw new Exception($"actor id is 0: {DCETRuntime.MongoHelper.ToJson(message)}");
            }

            string address = StartConfigComponent.Instance.GetProcessInnerAddress(IdGenerater.GetProcessId(actorId));
            Session session = NetInnerComponent.Instance.Get(address);
            message.ActorId = actorId & IdGenerater.HeadMask | IdGenerater.Head;
            message.RpcId = ++RpcId;

            var tcs = new TaskCompletionSource<IActorResponse>();
            requestCallback.Add(message.RpcId, new ActorMessageSender((response) =>
            {
                tcs.SetResult(response);
            }));
            session.Send(message);
            return tcs.Task;
        }

        public void RunMessage(IActorResponse response)
        {
            ActorMessageSender actorMessageSender;
            if (!requestCallback.TryGetValue(response.RpcId, out actorMessageSender))
            {
                Log.Error($"not found rpc, maybe request timeout, response message: {StringHelper.ToString(response)}");
                return;
            }
            requestCallback.Remove(response.RpcId);

            actorMessageSender.Callback(response);
        }
    }
}
