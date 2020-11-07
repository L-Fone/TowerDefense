using ET;

namespace ET
{
	
	public class SessionComponentAwakeSystem : AwakeSystem<SessionComponent>
	{
		public override void Awake(SessionComponent self)
		{
			self.Awake();
		}
	}

	public class SessionComponent: Entity
	{
		public static SessionComponent Instance;

		private Session session;

		public Session Session
		{
			get
			{
				return this.session;
			}
			set
			{
				this.session = value;
				
				//if (this.session != null)
				//{
				//	this.session.Parent = this;
				//}
			}
		}

		public void Awake()
		{
			Instance = this;
		}
		public static void Send(IMessage message)
		{
			Instance.Session.Send(message);
		}
		public static async ETTask<T> Call<T>(IRequest request,bool isMask = true) where T : IResponse
		{
			var ret = (T)await Instance.Session.Call(request);
			return ret;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

			this.Session.Dispose();
			this.Session = null;
			Instance = null;
		}
	}
}
