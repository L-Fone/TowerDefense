using BehaviorDesigner.Runtime;

namespace DCET
{
    [ObjectSystem]
    public class BehaviorManagerComponentUpdateSystem : UpdateSystem<BehaviorManagerComponent>
    {
        public override void Update(BehaviorManagerComponent self)
        {
            BehaviorManager.instance?.Update();
        }
    }

    [ObjectSystem]
    public class BehaviorManagerComponentLateUpdateSystem : LateUpdateSystem<BehaviorManagerComponent>
    {
        public override void LateUpdate(BehaviorManagerComponent self)
        {
            BehaviorManager.instance?.LateUpdate();
        }
    }

    public class BehaviorManagerComponent : Entity
    {
        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Dispose();

            BehaviorManager.instance?.OnDestroy();
        }
    }
}
