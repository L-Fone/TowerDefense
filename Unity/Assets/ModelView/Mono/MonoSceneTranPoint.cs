using ET.EventType;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class MonoSceneTranPoint:MonoBehaviour
	{
        public long Id;
        public Scene zoneScene;
		private void OnTriggerEnter2D(Collider2D collision)
		{
			Game.EventSystem.Publish(new OnEnterSceneTranPoint
            {
                zoneScene =zoneScene,
                collision =collision,
                Id =Id,
            }).Coroutine();
		}
	}
}