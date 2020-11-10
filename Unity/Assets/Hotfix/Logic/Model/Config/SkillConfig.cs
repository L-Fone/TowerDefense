using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace Cal.DataTable
{
	[Config]
	public partial class SkillConfigCategory : ACategory<SkillConfig>
	{
		public static SkillConfigCategory Instance;
		public SkillConfigCategory()
		{
			Instance = this;
		}
	}

	public partial class SkillConfig:IConfig
	{
		[BsonId]
		public long Id { get; set; }
		public string Name;
		public string Desc;
		public string info;
		public int CD;
		public int CastType;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float CastValue;
		public int PreSkillId;
		public int MaxLevel;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args0;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args1;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args2;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args3;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args4;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args5;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args6;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args7;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args8;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args9;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args10;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args11;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args12;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args13;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args14;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args15;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args16;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args17;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args18;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args19;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args20;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args29;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Args30;
		public int SkillType;
		public int LearnLevel;
		public int EffectId;
		public int DelayTime;
		public int HurtEffectId;
		public string Icon;
	}
}
