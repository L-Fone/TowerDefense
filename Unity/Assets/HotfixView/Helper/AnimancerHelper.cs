using ET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ET
{
    public class AnimancerHelper
    {
        public static MonoAnimancer GetAnimancers(Unit unit = null)
        {
            unit = unit??UnitComponent.MyUnit;
            return unit.GetComponent<UnitView>()?.monoAnimancer;
        }
    }
}
