using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCET
{
	[ObjectSystem]
	public class ActorMessageDispatcherComponentAwakeSystem : AwakeSystem<ActorMessageDispatcherComponent>
	{
		public override void Awake(ActorMessageDispatcherComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class ActorMessageDispatcherComponentLoadSystem : LoadSystem<ActorMessageDispatcherComponent>
	{
		public override void Load(ActorMessageDispatcherComponent self)
		{
			self.Load();
		}
	}

	[ObjectSystem]
	public class ActorMessageDispatcherComponentDestroySystem : DestroySystem<ActorMessageDispatcherComponent>
	{
		public override void Destroy(ActorMessageDispatcherComponent self)
		{
			self.Destroy();
		}
	}

	/// <summary>
	/// Actor消息分发组件
	/// </summary>
	public class ActorMessageDispatcherComponent : Entity
	{
		public static ActorMessageDispatcherComponent Instance;
		
		public readonly Dictionary<Type, IMActorHandler> ActorMessageHandlers = new Dictionary<Type, IMActorHandler>();


		public void Awake()
		{
			Load();
			Instance = this;
		}

		public void Load()
		{
			ActorMessageHandlers.Clear();

			HashSet<Type> types = Game.EventSystem.GetTypes(typeof(ActorMessageHandlerAttribute));
			foreach (Type type in types)
			{
				object obj = Activator.CreateInstance(type);

				IMActorHandler imHandler = obj as IMActorHandler;
				if (imHandler == null)
				{
					throw new Exception($"message handler not inherit IMActorHandler abstract class: {obj.GetType().FullName}");
				}

				Type messageType = imHandler.GetMessageType();
				ActorMessageHandlers.Add(messageType, imHandler);
			}
		}

		/// <summary>
		/// 分发actor消息
		/// </summary>
		public async Task Handle(Entity entity, Session session, object message)
		{
			if (!ActorMessageHandlers.TryGetValue(message.GetType(), out IMActorHandler handler))
			{
				throw new Exception($"not found message handler: {message}");
			}

			await handler.Handle(session, entity, message);
		}

		public void Destroy()
		{
			ActorMessageHandlers.Clear();
			Instance = null;
		}
	}
}