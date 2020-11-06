using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
	[BsonIgnoreExtraElements]
	public class RunServerConfig: AConfigComponent
	{
		public string IP = "";
	}
}