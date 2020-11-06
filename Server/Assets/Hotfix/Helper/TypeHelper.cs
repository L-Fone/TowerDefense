namespace DCET
{
	public static class TypeHelper
	{
		public static void InitAllType()
		{
			Game.EventSystem.AddAssemblyType("DCET.Core");
			Game.EventSystem.AddAssemblyType("DCET.Config");
			Game.EventSystem.AddAssemblyType("DCET.CoroutineLock");
			Game.EventSystem.AddAssemblyType("DCET.DB");
			Game.EventSystem.AddAssemblyType("DCET.Message");
			Game.EventSystem.AddAssemblyType("DCET.Numeric");
			Game.EventSystem.AddAssemblyType("DCET.Pathfinding");			
			Game.EventSystem.AddAssemblyType("DCET.BehaviorTree");
			Game.EventSystem.AddAssemblyType("DCET.Runtime");
			Game.EventSystem.AddAssemblyType("DCET.Hotfix");
			Game.EventSystem.AddAssemblyType("DCET.Benchmark");
		}
	}
}
