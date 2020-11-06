using BehaviorDesigner.Runtime;
using System;

namespace DCET
{
    public static class BehaviorTreeFactory
    {
        public static BehaviorTree Create(Entity domain, string name)
        {
            string path = $"../Config/BehaviorDesigner/{name}.json";

            try
            {
                var behavior = Behavior.Create(path);

                if(behavior != null)
                {
					var behaviorTree = EntityFactory.Create<BehaviorTree, Behavior>(domain, behavior);
					behaviorTree.AddComponent<BehaviorTreeVariableComponent>();
					return behaviorTree;
				}

                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"load behavior file fail, path: {path} {e}");
            }
        }
    }
}
