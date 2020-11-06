namespace DCET
{
    [Event(EventIdType.AfterScenesAdd)]
    public class AfterScenesAdd_CreateScene: AEvent
    {
        public override void Run()
        {
            foreach (StartConfig startConfig in StartConfigComponent.Instance.StartConfig.List)
            {
                SceneConfig sceneConfig = startConfig.GetComponent<SceneConfig>();
                SceneFactory.Create(Game.Scene, startConfig.Id, sceneConfig.Name, sceneConfig.SceneType);
            }
        }
    }
}