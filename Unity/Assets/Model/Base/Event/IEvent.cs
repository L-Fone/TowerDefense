using System;

namespace ET
{
    public interface IEvent
    {
        Type GetEventType();
    }

    [Event]
    public abstract class AEvent<A> : IEvent where A : struct
    {
        public Type GetEventType()
        {
            return typeof(A);
        }

        public abstract ETTask Run(A args);
    }
    [Event]
    public abstract class AEvent_Sync<A> : IEvent where A : struct
    {
        public Type GetEventType()
        {
            return typeof(A);
        }

        public abstract void Run(A args);
    }
    public interface IOldEvent
	{
		void Handle();
		void Handle(object a);
		void Handle(object a, object b);
		void Handle(object a, object b, object c);
	}

	public abstract class AOldEvent : IOldEvent
	{
		public void Handle()
		{
			this.Run();
		}

		public void Handle(object a)
		{
			Log.Error($"错误");
		}

		public void Handle(object a, object b)
		{
			Log.Error($"错误");
		}

		public void Handle(object a, object b, object c)
		{
			Log.Error($"错误");
		}

		public abstract void Run();
	}

	public abstract class AOldEvent<A>: IOldEvent
	{
		public void Handle()
		{
			Log.Error($"错误");
		}

		public void Handle(object a)
		{
			this.Run((A)a);
		}

		public void Handle(object a, object b)
		{
			Log.Error($"错误");
		}

		public void Handle(object a, object b, object c)
		{
			Log.Error($"错误");
		}

		public abstract void Run(A a);
	}

	public abstract class AOldEvent<A, B>: IOldEvent
	{
		public void Handle()
		{
			Log.Error($"错误");
		}

		public void Handle(object a)
		{
			Log.Error($"错误");
		}

		public void Handle(object a, object b)
		{
			this.Run((A)a, (B)b);
		}

		public void Handle(object a, object b, object c)
		{
			Log.Error($"错误");
		}

		public abstract void Run(A a, B b);
	}

	public abstract class AOldEvent<A, B, C>: IOldEvent
	{
		public void Handle()
		{
			Log.Error($"错误");
		}

		public void Handle(object a)
		{
			Log.Error($"错误");
		}

		public void Handle(object a, object b)
		{
			Log.Error($"错误");
		}

		public void Handle(object a, object b, object c)
		{
			this.Run((A)a, (B)b, (C)c);
		}

		public abstract void Run(A a, B b, C c);
	}
}