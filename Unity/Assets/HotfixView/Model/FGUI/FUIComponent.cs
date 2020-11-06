using ET;
using ET;
using FairyGUI;
using System.Collections.Generic;

namespace ET
{
    
	public class FUIComponentAwakeSystem : AwakeSystem<FUIComponent>
	{
		public override void Awake(FUIComponent self)
		{
			self.Root = EntityFactory.CreateWithParent<FUI, GObject>(self.Domain, GRoot.inst);
			FUIComponent.Instance = self;
		}
	}

	/// <summary>
	/// 管理所有顶层UI, 顶层UI都是GRoot的孩子
	/// </summary>
	public class FUIComponent: Entity
	{
		public static FUIComponent Instance { get; set; }

		public FUI Root;
		
		public override void Dispose()
		{
			if (IsDisposed)
			{
				return;
			}

			base.Dispose();

            Root.Dispose();
            Root = null;
		}


        public async ETTask<FUI> Create(string uiType)
        {
            FUI ui = await UIEventComponent.Instance.OnCreate(this, uiType);
            //self.UIs.Add(uiType, ui);

            //Root.Add(ui);
            Root?.Add(ui, true);
            return ui;
        }
        public void Add(FUI ui, bool asChildGObject)
		{
			Root?.Add(ui, asChildGObject);
		}
		
		public void Remove(string name)
		{
			Root?.Remove(name);
		}
		
		/// <summary>
		/// 通过名字获得FUI
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public FUI Get(string name)
		{
			return Root?.Get(name);
        }
		
		/// <summary>
		/// 通过ID获得FUI
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public FUI Get(long id)
		{
			return Root?.Get(id.ToString());
		}

        public IEnumerable<FUI> GetAll()
        {
            return Root?.GetAll();
        }

        public void Clear()
        {
            var childrens = GetAll();

            if(childrens != null)
            {
                foreach (var fui in childrens)
                {
                    Remove(fui.Name);
                }
            }
        }
	}
}