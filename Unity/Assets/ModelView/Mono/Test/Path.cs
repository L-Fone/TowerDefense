using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public class WayPath
    {
        public List<Vector3> LookPoints{ get;private set; }
        public Line[] TurnBoundaries{ get;private set; }
        public int FinishLineIndex{ get;private set; }
        public int SlowDownIndex{ get;private set; }
        public void Init(List<Vector3> wayPoints, Vector3 strtPos, float turnDis,float stopDis)
        {
            LookPoints = wayPoints;
            TurnBoundaries = new Line[wayPoints.Count];
            FinishLineIndex = TurnBoundaries.Length - 1;

            Vector2 previoursPoint = V3ToV2(strtPos);
            int index = 0;
            foreach (var item in LookPoints)
            {
                Vector2 currPoint = V3ToV2(item);
                Vector2 dirToCurrPoint = (currPoint - previoursPoint).normalized;
                Vector2 turnBoundaryPoint =
                    index == FinishLineIndex ?
                    currPoint :
                    currPoint - dirToCurrPoint * turnDis;
                TurnBoundaries[index++] = new Line(turnBoundaryPoint, previoursPoint - dirToCurrPoint * turnDis);
                previoursPoint = turnBoundaryPoint;
            }
            float disFromEndPoint = 0;
            for (int i = LookPoints.Count-1; i >0; i--)
            {
                disFromEndPoint += Vector3.SqrMagnitude(LookPoints[i] - LookPoints[i - 1]);
                if (disFromEndPoint > stopDis * stopDis)
                {
                    SlowDownIndex = i;
                    break;
                }
            }
        }
        Vector2 V3ToV2(Vector3 vector3) => new Vector2(vector3.x, vector3.y);

        public void DrawWithGizmos()
        {
            Gizmos.color = Color.black;
            foreach (var item in LookPoints)
            {
                Gizmos.DrawCube(item + Vector3.up, Vector3.one);
            }
            Gizmos.color = Color.white;
            foreach (var item in TurnBoundaries)
            {
                item.DrawWithGizmos(10);
            }
        }
    }

}
