using ET;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class UnitView : Entity
    {
        public GameObject gameObject;
        public Transform transform;

        public float yDelta;
        public float xDelta;

        public SpriteRenderer spriteRenderer;

        public SpriteAnimator spriteAnimator;

        public Transform HeadPoint;
        public Transform FootPoint;
        public Unit unit;
        /// <summary>
        /// 父物体坐标
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value + new Vector3(xDelta, yDelta, 0);
            }
        }
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
        public Vector3 AtkPosition
        {
            get
            {
                if (FootPoint && HeadPoint)
                    return new Vector3(FootPoint.position.x, (HeadPoint.position.y + FootPoint.position.y) / 2, FootPoint.position.z);
                else
                {
                    Log.Error($"HeadPoint = {HeadPoint} FootPoint = {FootPoint}");
                    return default;
                }
            }
        }
    }
}
