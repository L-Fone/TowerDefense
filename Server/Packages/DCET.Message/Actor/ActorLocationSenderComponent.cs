using System;
using System.Threading.Tasks;

namespace DCET
{
    [ObjectSystem]
    public class ActorLocationSenderComponentAwakeSystem : AwakeSystem<ActorLocationSenderComponent>
    {
        public override void Awake(ActorLocationSenderComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class ActorLocationSenderComponentDestroySystem : DestroySystem<ActorLocationSenderComponent>
    {
        public override void Destroy(ActorLocationSenderComponent self)
        {
            self.Destroy();
        }
    }

    public class ActorLocationSenderComponent: Entity
    {
        [NoMemoryCheck]
        public long TIMEOUT_TIME = 10 * 1000;
        
        public static ActorLocationSenderComponent Instance { get; set; }

        public long CheckTimer;


        public void Check()
        {
            using (ListComponent<long> list = EntityFactory.Create<ListComponent<long>>(Domain))
            {
                long timeNow = TimeHelper.CurrentLocalMilliseconds();
                foreach ((long key, Entity value) in Children)
                {
                    ActorLocationSender actorLocationMessageSender = (ActorLocationSender)value;

                    if (timeNow > actorLocationMessageSender.LastSendOrRecvTime + TIMEOUT_TIME)
                    {
                        list.List.Add(key);
                    }
                }

                foreach (long id in list.List)
                {
                    Remove(id);
                }
            }
        }

        private ActorLocationSender Get(long id)
        {
            if (id == 0)
            {
                throw new Exception($"actor id is 0");
            }
            if (Children.TryGetValue(id, out Entity actorLocationSender))
            {
                return (ActorLocationSender)actorLocationSender;
            }

            actorLocationSender = EntityFactory.CreateWithId<ActorLocationSender>(Domain, id);
            actorLocationSender.Parent = this;
            return (ActorLocationSender)actorLocationSender;
        }

        private void Remove(long id)
        {
            if (!Children.TryGetValue(id, out Entity actorMessageSender))
            {
                return;
            }
            actorMessageSender.Dispose();
        }

        public void Send(long entityId, IActorLocationMessage message)
        {
            ActorLocationSender actorLocationSender = Get(entityId);
            actorLocationSender.Send(message);
        }

        public async Task<IActorResponse> Call(long entityId, IActorLocationRequest message)
        {
            ActorLocationSender actorLocationSender = Get(entityId);
            return await actorLocationSender.Call(message);
        }

        public void Awake()
        {
            // 每10s扫描一次过期的actorproxy进行回收,过期时间是1分钟
            // 可能由于bug或者进程挂掉，导致ActorLocationSender发送的消息没有确认，结果无法自动删除，每一分钟清理一次这种ActorLocationSender
            CheckTimer = TimerComponent.Instance.NewRepeatedTimer(10 * 1000, Check);
            Instance = this;
        }

        public void Destroy()
        {
            TimerComponent.Instance.Remove(CheckTimer);
            CheckTimer = 0;
            Instance = null;
        }
    }
}