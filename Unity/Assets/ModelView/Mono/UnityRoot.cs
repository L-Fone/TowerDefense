using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ET
{
	public class UnityRoot:MonoBehaviour
	{
		public static UnityRoot Instance;
        public Transform[] GameObjectParentGroup;
        public Transform ObjPoolParent;
        [SerializeField]
        public GameObjectPoolEntity[] GameObjectPoolGroups;

        private void Awake()
		{
			Instance = this;
            DontDestroyOnLoad(this);

        }
	}
}