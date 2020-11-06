using System.Threading.Tasks;

namespace DCET
{
	[ObjectSystem]
	public class MailBoxComponentAwakeSystem : AwakeSystem<MailBoxComponent>
	{
		public override void Awake(MailBoxComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class MailBoxComponentAwake1System : AwakeSystem<MailBoxComponent, MailboxType>
	{
		public override void Awake(MailBoxComponent self, MailboxType mailboxType)
		{
			self.Awake(mailboxType);
		}
	}

	/// <summary>
	/// 挂上这个组件表示该Entity是一个Actor,接收的消息将会队列处理
	/// </summary>
	public class MailBoxComponent: Entity
	{
		// Mailbox的类型
		public MailboxType MailboxType;
		
		public void Awake(MailboxType mailboxType)
		{
			MailboxType = mailboxType;
		}

		public void Awake()
		{
			MailboxType = MailboxType.MessageDispatcher;
		}

		public async Task Handle(Session session, IActorMessage message)
		{
			using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.Mailbox, message.ActorId))
			{
				switch (MailboxType)
				{
					case MailboxType.GateSession:
						IActorMessage iActorMessage = message as IActorMessage;
						// 发送给客户端
						Session clientSession = Parent as Session;
						iActorMessage.ActorId = 0;
						clientSession.Send(iActorMessage);
						break;
					case MailboxType.MessageDispatcher:
						await ActorMessageDispatcherComponent.Instance.Handle(Parent, session, message);
						break;
					case MailboxType.UnOrderMessageDispatcher:
						HandleInner(session, message);
						break;
				}
			}
		}

		private async void HandleInner(Session session, IActorMessage message)
		{
			await ActorMessageDispatcherComponent.Instance.Handle(Parent, session, message);
		}
	}
}