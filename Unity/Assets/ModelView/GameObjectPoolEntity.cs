using System;
using System.Collections;
using System.Collections.Generic;

using PathologicalGames;
using UnityEngine;

namespace ET
{
	[System.Serializable]
	public class GameObjectPoolEntity
	{
		public byte PoolId;

		public string PoolName;

		[Tooltip("是否开启自动清理")]
		public bool CullDespawned = true;

		[Tooltip("自动清理保留数量")]
		public int CullAbove = 5;

		[Tooltip("清理间隔")]
		public int CullDelay = 2;

		[Tooltip("每次清理的数量")]
		public int CullMaxPerPass = 2;

		[Tooltip("对应的游戏物体对象池")]
		public SpawnPool Pool;
	}
}