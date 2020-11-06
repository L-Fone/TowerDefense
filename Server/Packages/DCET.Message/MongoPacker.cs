using System;
using System.IO;

namespace DCET
{
	public class MongoPacker: IMessagePacker
	{
		public void SerializeTo(object obj, MemoryStream stream)
		{
			DCETRuntime.MongoHelper.ToBson(obj, stream);
		}

		public object DeserializeFrom(object instance, MemoryStream stream)
		{
			return DCETRuntime.MongoHelper.FromBson(instance, stream);
		}
	}
}
