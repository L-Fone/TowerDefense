using MongoDB.Bson.Serialization.Attributes;

namespace Cal.DataTable
{
	public class RobotAccount
	{
		[BsonId]
		public long Id { get; set; }
		public string Account;
		public string PassWord;
	}
}
