using System;
using System.Collections.Generic;

namespace ET
{
	
	public class ConfigComponentAwakeSystem : AwakeSystem<ConfigComponent>
	{
		public override void Awake(ConfigComponent self)
		{
			self.Awake();
		}
	}

	
	public class ConfigComponentLoadSystem : LoadSystem<ConfigComponent>
	{
		public override void Load(ConfigComponent self)
		{
			self.Load();
		}
	}
	public class ConfigComponentDestroySystem : DestroySystem<ConfigComponent>
    {
    	public override void Destroy(ConfigComponent self)
    	{
    		self.Destroy();
    	}
    }

	/// <summary>
	/// Config组件会扫描所有的有ConfigAttribute标签的配置,加载进来
	/// </summary>
	public class ConfigComponent: Entity
	{
		public static ConfigComponent instance;
		private Dictionary<Type, ACategory> allConfig = new Dictionary<Type, ACategory>();

		public void Awake()
		{
			instance = this;
			this.Load();
		}

		public void Load()
		{
            this.allConfig.Clear();
            HashSet<Type> types = Game.EventSystem.GetTypes(typeof(ConfigAttribute));

            foreach (Type type in types)
            {
                object obj = Activator.CreateInstance(type);

                ACategory iCategory = obj as ACategory;
                if (iCategory == null)
                {
                    throw new Exception($"class: {type.Name} not inherit from ACategory");
                }
                iCategory.BeginInit();
                iCategory.EndInit();

                this.allConfig[iCategory.ConfigType] = iCategory;
            }
        }

		public IConfig GetOne(Type type)
		{
			ACategory configCategory;
			if (!this.allConfig.TryGetValue(type, out configCategory))
			{
				throw new Exception($"ConfigComponent not found key: {type.FullName}");
			}
			return configCategory.GetOne();
		}

		public IConfig Get(Type type, long id)
		{
			ACategory configCategory;
			if (!this.allConfig.TryGetValue(type, out configCategory))
			{
				throw new Exception($"ConfigComponent not found key: {type.FullName}");
			}

			return configCategory.TryGet(id);
		}


		public IEnumerable<T> GetAll<T>(Type type)
		{
			ACategory configCategory;
			if (!this.allConfig.TryGetValue(type, out configCategory))
			{
				throw new Exception($"ConfigComponent not found key: {type.FullName}");
			}
			return configCategory.GetAll<T>();
		}

		public ACategory GetCategory(Type type)
		{
			ACategory configCategory;
			bool ret = this.allConfig.TryGetValue(type, out configCategory);
			return ret ? configCategory : null;
		}

        internal void Destroy()
        {
			allConfig.Clear();
			instance = null;
        }
    }
}