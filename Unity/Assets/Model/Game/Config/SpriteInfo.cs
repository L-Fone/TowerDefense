using System;
using System.Collections.Generic;

namespace ET
{
    [Config]
    public partial class SpriteInfoConigCategory : ACategory<SpriteInfoConig>
    {
        public static SpriteInfoConigCategory Instance;
        public SpriteInfoConigCategory()
        {
            Instance = this;
        }
    }

    public class SpriteInfo
    {
        public string name;
        public System.Numerics.Vector2 pivot;
        public float x;
        public float y;
        public float width;
        public float height;
    }

    public class SpriteInfoConig : IConfig
    {
        public long Id { get; set; }
        public List<SpriteInfo> list;

    }
}
