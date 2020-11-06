namespace DCET
{
	[Config((int)(AppType.ClientH |  AppType.ClientM | AppType.Gate | AppType.Map))]
	public partial class BehaviorTreeConfigCategory : ACategory<BehaviorTreeConfig>
	{
	}

	public class BehaviorTreeConfig: IConfig
	{
		public long _id {get; set;}
		public string Name {get; set;}
		public string Description {get; set;}
		public string ComponentName {get; set;}
		public string ParameterList {get; set;}
	}
}
