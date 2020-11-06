using BehaviorDesigner.Runtime;

namespace DCET
{
    [ObjectSystem]
    public class BehaviorTreeAwakeSystem : AwakeSystem<BehaviorTree, Behavior>
    {
        public override void Awake(BehaviorTree self, Behavior behaviorTree)
        {
            self.Awake(behaviorTree);
        }
    }

    public class BehaviorTree : Entity
    {
        public Behavior Behavior;

        public void Awake(Behavior behaviorTree)
        {
            Behavior = behaviorTree;
        }

        public void EnableBehaior()
        {
            Behavior?.EnableBehavior();
        }

        public void DisableBehavior()
        {
            Behavior?.DisableBehavior();
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Dispose();

            Behavior = null;
        }
    }
}
