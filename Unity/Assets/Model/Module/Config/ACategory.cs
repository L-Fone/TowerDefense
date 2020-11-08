using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
	public abstract class ACategory : Object
	{
		public abstract Type ConfigType { get; }
		public abstract IConfig GetOne();
		public abstract IEnumerable<T> GetAll<T>();
		public abstract IConfig TryGet(long type);
	}

	/// <summary>
	/// 管理该所有的配置
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ACategory<T> : ACategory where T : IConfig
	{
		protected Dictionary<long, T> dict;

		public override void BeginInit()
		{
            string configStr = ConfigHelper.GetText(typeof(T).Name);

            try
            {
                this.dict = ConfigHelper.ToObject<Dictionary<long, T>>(configStr);
            }
            catch (Exception e)
            {
                throw new Exception($"parser json fail: {configStr}", e);
            }
        }

		public override Type ConfigType=> typeof(T);

		public override void EndInit()
		{
		}

		public override IConfig TryGet(long type)
		{
            if (!this.dict.TryGetValue(type, out T t))
            {
                return null;
            }
            return t;
		}

		public override IEnumerable<TT> GetAll<TT>()
		{
			return this.dict.Values as IEnumerable<TT>;
		}

		public override IConfig GetOne()
		{
			return this.dict.Values.First();
		}
	}
}