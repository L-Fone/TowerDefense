//using System;
//using ET;

namespace ET
{
	//public abstract class AMHandler<Message> : IMHandler where Message: class
	//{
	//	protected abstract ETTask Run(ET.Session session, Message message);

	//	public async ETVoid Handle(ET.Session session, object msg)
	//	{
	//		Message message = msg as Message;
	//		if (message == null)
	//		{
	//			Log.Error($"消息类型转换错误: {msg.GetType().Name} to {typeof(Message).Name}");
	//		}

	//		try
	//		{
	//			await this.Run(session, message);
	//		}
	//		catch (Exception e)
	//		{
	//			Log.Error(e);
	//		}
	//	}

	//	public Type GetMessageType()
	//	{
	//		return typeof(Message);
	//	}
	//}
}