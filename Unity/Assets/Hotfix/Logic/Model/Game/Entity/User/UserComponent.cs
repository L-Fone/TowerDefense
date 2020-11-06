using ET;
using System.Collections.Generic;

namespace ET
{
	
	public class UserComponentAwakeSystem : AwakeSystem<UserComponent>
	{
		public override void Awake(UserComponent self)
		{
			self.Awake();
		}
	}
	
	public class UserComponent : Entity
	{

		public static UserComponent Instance { get; private set; }

		private User myUser;

		public static User MyUser
		{
			get
			{
				return Instance.myUser;
			}
			set
			{
				Instance.myUser = value;
			}
		}
		
		private readonly Dictionary<long, User> idPlayers = new Dictionary<long, User>();

		public void Awake()
		{
			Instance = this;
		}
		
		public void Add(User player)
		{
			this.idPlayers.Add(player.Id, player);
		}

		public User Get(long id)
		{
			User player;
			this.idPlayers.TryGetValue(id, out player);
			return player;
		}

		public void Remove(long id)
		{
			this.idPlayers.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idPlayers.Count;
			}
		}

		public IEnumerable<User> GetAll()
		{
			return this.idPlayers.Values;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			foreach (User player in this.idPlayers.Values)
			{
				player.Dispose();
			}

			Instance = null;
		}
	}
}