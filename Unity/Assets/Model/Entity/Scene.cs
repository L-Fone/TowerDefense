using System;

namespace ET
{
    public enum SceneName
    {
        Logic,
        View
    }
    public enum SceneType
    {
        Process = 0,
        Manager = 1,
        Realm = 2,
        Gate = 3,
        Http = 4,
        Location = 5,
        Map = 6,

        // 客户端Model层
        Client = 30,
        Zone = 31,
        Login = 32,
        Logic = 33,
        View = 34,
    }
    public sealed class Scene : Entity
    {
        public int Zone { get; }
        public SceneType SceneType { get; }
        public string Name { get; }

        public Scene(long id, int zone, SceneType sceneType, string name)
        {
            this.Id = id;
            this.InstanceId = id;
            this.Zone = zone;
            this.SceneType = sceneType;
            this.Name = name;
            this.IsCreate = true;
        }

        public Scene Get(long id)
        {
            return (Scene)this.Children?[id];
        }

        public new Entity Domain
        {
            get
            {
                return this.domain;
            }
            set
            {
                this.domain = value;
            }
        }

        public new Entity Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (value == null)
                {
                    this.parent = this;
                    return;
                }
                this.parent = value;
                this.parent.Children.Add(this.Id, this);
#if UNITY_EDITOR
                this.ViewGO.transform.SetParent(this.parent.ViewGO.transform, false);
#endif
            }
        }
    }
}