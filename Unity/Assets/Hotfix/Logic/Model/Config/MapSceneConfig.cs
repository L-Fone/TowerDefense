using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace Cal.DataTable
{
	[Config]
	public partial class MapSceneConfigCategory : ACategory<MapSceneConfig>
	{
		public static MapSceneConfigCategory Instance;
		public MapSceneConfigCategory()
		{
			Instance = this;
		}
	}

	public partial class MapSceneConfig:IConfig
	{
		[BsonId]
		public long Id { get; set; }
		public string Name;
		public string Desc;
	}
}
