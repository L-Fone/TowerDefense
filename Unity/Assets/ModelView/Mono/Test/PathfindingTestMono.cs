
using DG.Tweening;
using GridPathfindingSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class PathfindingTestMono : MonoBehaviour
    {
        [SerializeField]
        private Transform left;
        [SerializeField]
        private Transform right;
        [SerializeField]
        private Transform walkAble;
        [SerializeField]
        private Transform unwalkAble;
        private GridPathfinding gridPathfinding;

        [SerializeField]
        private Transform start, end, pathPrefab;

        [SerializeField]
        private float turnSpeed = 5, moveSpeed = 50;
        private int pathIndex;
        private bool followParthing;
        private WayPath path;
        private Transform tran;

        private void Start()
        {
            gridPathfinding = new GridPathfindingSystem.GridPathfinding(new Vector3(left.position.x, left.position.z, 0), new Vector3(right.position.x, right.position.z, 0), 1.5f);
            gridPathfinding.Raycast3DWalkable();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                tran = GameObject.Instantiate(pathPrefab, start.position, Quaternion.identity, GameObject.Find("Game").transform).transform;
                var pathRoute = gridPathfinding.GetPathRoute(new Vector3(start.position.x, start.position.z), new Vector3(end.position.x, end.position.z));
                path = new WayPath();
                var list = pathRoute.pathVectorList;
                path.Init(list, list[0], 0.2f, 1f);
                pathIndex = 0;
                followParthing = true;
                var first = path.LookPoints[0];
                tran.LookAt(new Vector3(first.x, 0, first.y));

            }
            ///y->-z
            ///x->x
            ///z->y
            ///(x,0,z)->(x,-z=0,y)
            ///pos =（x,z,-y）
            if (followParthing)
            {

                while (path.TurnBoundaries[pathIndex].HasCrossLine(new Vector2(tran.position.x, tran.position.z)))
                {
                    if (pathIndex >= path.FinishLineIndex)
                    {
                        followParthing = false;
                        return;
                    }
                    else
                        pathIndex++;
                }
                Vector3 point = path.LookPoints[pathIndex];
                Quaternion quaternion = Quaternion.LookRotation(new Vector3(point.x, 0, point.y) - new Vector3(tran.position.x, 0, tran.position.z));
                tran.rotation = Quaternion.Lerp(tran.rotation, quaternion, turnSpeed * Time.deltaTime);
                tran.Translate(new Vector3(0, 0, 1) * Time.deltaTime * moveSpeed, Space.Self);
            }

        }

    }
}
