using ET;
using UnityEngine;

namespace ET
{
    
    public class CameraComponentAwakeSystem : AwakeSystem<CameraComponent, float, float>
    {
        public override void Awake(CameraComponent self, float a, float b)
        {
            self.Awake(a, b);
        }
    }

    
    public class CameraComponentLateUpdateSystem : LateUpdateSystem<CameraComponent>
    {
        public override void LateUpdate(CameraComponent self)
        {
            self.LateUpdate();
        }
    }

    public class CameraComponent : Entity
    {
        public Unit Unit { get; set; }

        public Camera MainCamera { get; private set; }

        private Transform m_Trans;

        private float m_ActualMinPos;
        private float m_ActualMaxPos;
        private float m_CameraY;
        private float m_CameraZ;
        public void Awake(float minX, float maxX)
        {
            Unit = GetParent<Unit>();
            this.MainCamera = Camera.main;
            m_Trans = MainCamera.transform;
            m_CameraY = m_Trans.position.y;
            m_CameraZ = m_Trans.position.z;
            m_ActualMinPos = minX + Screen.width * MainCamera.orthographicSize / Screen.height;
            m_ActualMaxPos = maxX - Screen.width * MainCamera.orthographicSize / Screen.height;
        }

        public void LateUpdate()
        {
            // 摄像机每帧更新位置
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (!Unit.IsFight)
                m_Trans.position = new Vector3(Mathf.Clamp(Unit.Position.x, m_ActualMinPos, m_ActualMaxPos), m_CameraY, m_CameraZ);
        }
    }
}
