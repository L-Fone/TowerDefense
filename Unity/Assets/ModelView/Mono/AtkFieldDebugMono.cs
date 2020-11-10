using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public class AtkFieldDebugMono:UnityEngine.MonoBehaviour
    {
        public float atkField;

      
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position, atkField);
        }
    }
}
