using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace Cal.DataTable
{
	[Config]
	public partial class LevelConfigCategory : ACategory<LevelConfig>
	{
		public static LevelConfigCategory Instance;
		public LevelConfigCategory()
		{
			Instance = this;
		}
	}

	public partial class LevelConfig:IConfig
	{
		[BsonId]
		public long Id { get; set; }
		public string Desc;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float[] InitPos;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float InitAngle;
	}
}
