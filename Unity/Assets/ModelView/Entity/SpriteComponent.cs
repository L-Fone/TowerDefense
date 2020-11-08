using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class SpriteComponentAwakeSystem : AwakeSystem<SpriteComponent>
    {
    	public override void Awake(SpriteComponent self)
    	{
    		self.Awake();
    	}
    }
    public class SpriteComponent:Entity
    {
        private static SpriteComponent inst;

        private Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();

        internal void Awake()
        {
            inst = this;
        }
        public static Sprite Create(SpriteInfo spriteInfo ,Texture2D texture2D)
        {
            if(!inst.spriteDic.TryGetValue(spriteInfo.name,out var sprite))
            {
                sprite = Sprite.Create(texture2D, new Rect(spriteInfo.x, spriteInfo.y, spriteInfo.width, spriteInfo.height), new Vector2(0.5f, 0.5f));
                inst.spriteDic.Add(spriteInfo.name, sprite);
            }
            return sprite;
        }
    }
}
