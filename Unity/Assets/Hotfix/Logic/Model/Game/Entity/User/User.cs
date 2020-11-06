using ET;
using System;
namespace ET

{

    public class UserSystem : AwakeSystem<User, string>
    {
        public override void Awake(User self, string a)
        {
            self.Awake(a);
        }
    }
    /// <summary>
    /// 游戏核心类
    /// </summary>
    public sealed class User : Entity
    {
        public string UserName { get; set; }
        public void Awake(string userName)
        {
            UserName = userName;
        }
    }
}