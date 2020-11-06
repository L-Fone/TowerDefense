using ET;

namespace ET
{
    public static class UserFactory
    {
        public static User Create(Scene scene,long id)
        {
            UserComponent playerComponent = scene.GetComponent<UserComponent>();
            User user = EntityFactory.CreateWithParentAndId<User>(playerComponent, id);
            playerComponent.Add(user);
            return user;
        }
    }
}