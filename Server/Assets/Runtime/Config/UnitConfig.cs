namespace DCET
{
	[Config((int)(AppType.ClientH |  AppType.ClientM | AppType.Gate | AppType.Map))]
	public partial class UnitConfigCategory : ACategory<UnitConfig>
	{
	}

	public class UnitConfig: IConfig
	{
		public long _id {get; set;}
		public string Name {get; set;}
		public string Desc {get; set;}
		public int Position {get; set;}
		public int Height {get; set;}
		public int Weight {get; set;}
	}
}
