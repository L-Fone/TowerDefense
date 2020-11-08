using System;
using System.Collections.Generic;

namespace ET
{
    public class LevelInfo
    {
        public List<System.Numerics.Vector3> path = new List<System.Numerics.Vector3>();
        public System.Numerics.Vector3 initPos;
        public System.Numerics.Vector3 endPos;
        public List<System.Numerics.Vector3> towerList = new List<System.Numerics.Vector3>();
    }
}
