using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETEditor
{
	public class ScriptTemplate
	{
		public const string Entity =
@"using System;
using 另一个命名空间;

namespace 当前命名空间
{
	
	public class 类名AwakeSystem : AwakeSystem<类名>
	{
		public override void Awake(类名 self)
		{
			self.Awake();
		}
	}

	
	public class 类名StartSystem : StartSystem<类名>
	{
		public override void Start(类名 self)
		{
			self.Start();
		}
	}

	
	public class 类名UpdateSystem : UpdateSystem<类名>
	{
		public override void Update(类名 self)
		{
			self.Update();
		}
	}

	
	public class 类名DestroySystem : DestroySystem<类名>
	{
		public override void Destroy(类名 self)
		{
			self.Destroy();
		}
	}
	
	public class 类名: Entity
	{
		public void Awake()
		{

		}

		public void Start()
		{

		}

		public void Update()
		{

		}

		public void Destroy()
		{

		}
	}
}";

		public const string UnitEntity =
@"using System;
using 另一个命名空间;
using UnityEngine;

namespace 当前命名空间
{
	
	public class 类名AwakeSystem : AwakeSystem<类名, GameObject>
	{
		public override void Awake(类名 self, GameObject go)
		{
			self.Awake(go);
		}
	}

	
	public class 类名StartSystem : StartSystem<类名>
	{
		public override void Start(类名 self)
		{
			self.Start();
		}
	}

	
	public class 类名UpdateSystem : UpdateSystem<类名>
	{
		public override void Update(类名 self)
		{
			self.Update();
		}
	}

	
	public class 类名DestroySystem : DestroySystem<类名>
	{
		public override void Destroy(类名 self)
		{
			self.Destroy();
		}
	}
	
	[HideInHierarchy]
	public class 类名: Entity
	{
		public void Awake(GameObject go)
		{
			ViewGO = go;
			ViewGO.AddComponent<ComponentView>().Component = this;
		}

		public void Start()
		{

		}

		public void Update()
		{

		}

		public void Destroy()
		{

		}
	}
}";

		public const string Normal =
@"using System;
using 另一个命名空间;

namespace 当前命名空间
{
	public class 类名
	{

	}
}";

		public const string UIScript =
@"using System;
using ET;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	
	public class 类名AwakeSystem : AwakeSystem<类名>
	{
		public override void Awake(类名 self)
		{
			self.Awake();
		}
	}

	
	public class 类名StartSystem : StartSystem<类名>
	{
		public override void Start(类名 self)
		{
			self.Start();
		}
	}

	
	public class 类名UpdateSystem : UpdateSystem<类名>
	{
		public override void Update(类名 self)
		{
			self.Update();
		}
	}

	
	public class 类名DestroySystem : DestroySystem<类名>
	{
		public override void Destroy(类名 self)
		{
			self.Destroy();
		}
	}
	
	public partial class 类名: Entity
	{
		public void Awake()
		{
			GetReference();
		}

		public void Start()
		{

		}

		public void Update()
		{

		}

		public void Destroy()
		{

		}
	}
}";

		public const string UIReference =
@"using System;
using ET;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	public partial class 类名
	{
		public ReferenceCollector Collector;
		组件

		public void GetReference()
		{
			Collector = GetParent<UI>().ViewGO.GetComponent<ReferenceCollector>();
			获取引用
		}
	}
}";

		public const string UIType =
@"using System;

namespace ETHotfix
{
    public static class UIType
    {
		面板
	}
}";

		public const string AEvent =
@"using 另一个命名空间;

namespace 当前命名空间
{
	[OldEvent(EventIdType.事件类型)]
	public class 脚本名: AEvent
	{
		public override void Run()
		{

		}
	}
}";

		public const string EventIdType =
@"namespace 命名空间
{
	public static class EventIdType
	{
		事件类型
	}
}";

		public const string AMRpcHanler =
@"using System;
using System.Net;
using ET;

namespace ETHotfix
{
	[MessageHandler]
	public class 请求Handler : AMRpcHandler<请求, 响应>
	{
		protected override async ETTask Run(Session session, 请求 request, 响应 response, Action reply)
		{
			reply();
		}
	}
}";

		public const string AMHandler =
@"using ET;

namespace ETHotfix
{
	[MessageHandler]
	public class 消息类型Handler : AMHandler<消息类型>
	{
		protected override async ETTask Run(ET.Session session, 消息类型 message)
		{
			await ETTask.CompletedTask;
		}
	}
}"; 
		public const string ET6AMRpcHanler =
 @"using System;

namespace ET
{
	[ActorMessageHandler]
	public class 请求Handler : AMActorLocationRpcHandler<Unit, 请求, 响应>
	{
		protected override async ETTask Run(Unit unit, 请求 request, 响应 response, Action reply)
		{
			reply();
			await ETTask.CompletedTask;
		}
	}
}";

		public const string ET6AMHandler =
@"

namespace ET
{
	[ActorMessageHandler]
	public class 消息类型Handler : AMActorHandler<消息类型>
	{
		protected override async ETTask Run(Session session, 消息类型 message)
		{
			await ETTask.CompletedTask;
		}
	}
}";
	}
}
