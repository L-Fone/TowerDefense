using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace Cal.DataTable
{
	[Config]
	public partial class RoleConfigCategory : ACategory<RoleConfig>
	{
		public static RoleConfigCategory Instance;
		public RoleConfigCategory()
		{
			Instance = this;
		}
	}

	public partial class RoleConfig:IConfig
	{
		[BsonId]
		public long Id { get; set; }
		public string Name;
		public string Desc;
		public int UpgradeId;
		public int PrefabId;
		public string AsltasPath;
		public int Frame;
		public int Atk;
		public int Def;
		public int Hp;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float Spd;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float AtkField;
		[BsonRepresentation(MongoDB.Bson.BsonType.Double, AllowTruncation = true)]
		public float AtkInterval;
	}
}
