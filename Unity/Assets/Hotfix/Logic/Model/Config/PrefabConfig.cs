using ET;
using MongoDB.Bson.Serialization.Attributes;

namespace Cal.DataTable
{
	[Config]
	public partial class PrefabConfigCategory : ACategory<PrefabConfig>
	{
		public static PrefabConfigCategory Instance;
		public PrefabConfigCategory()
		{
			Instance = this;
		}
	}

	public partial class PrefabConfig:IConfig
	{
		[BsonId]
		public long Id { get; set; }
		public string Name;
		public string Desc;
		public string AssetPath;
		public int PoolId;
		public bool CullDespawned;
		public int CullAbove;
		public int CullDelay;
		public int CullMaxPerPass;
	}
}
