using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public struct Line
    {
        private const float VerticalLineGradient = 1e5f;
        private float gradient;
        private float y_intercept;

        private float gradientPerpendicular;
        private Vector2 _pointOnLine_1;
        private Vector2 _pointOnLine_2;
        private bool approachSide ;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pointOnLine">所在点</param>
        /// <param name="pointPerpendicularToLine">逼近点</param>
        public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
        {
            float dx = pointOnLine.x - pointPerpendicularToLine.x;
            float dy = pointOnLine.y - pointPerpendicularToLine.y;
            if (dx == 0)
                gradientPerpendicular = VerticalLineGradient;
            else gradientPerpendicular = dy / dx;
            if (gradientPerpendicular == 0)
                gradient = VerticalLineGradient;
            else gradient = -1 / gradientPerpendicular;
            y_intercept = pointOnLine.y - gradient * pointOnLine.x;
            _pointOnLine_1 = pointOnLine;
            _pointOnLine_2 = pointOnLine + new Vector2(1, gradient);
            approachSide = false;
            approachSide = GetSide(pointPerpendicularToLine);
        }
        private bool GetSide(Vector2 p) => (p.x - _pointOnLine_1.x)*(_pointOnLine_2.y - _pointOnLine_1.y) > (_pointOnLine_2.x - _pointOnLine_1.x) *  (p.y - _pointOnLine_1.y);

        public bool HasCrossLine(Vector2 p)=>  GetSide(p) != approachSide;

        public float DistanceFromPoint(Vector2 p)
        {
            float yIterceptPerpendicular = p.y - gradientPerpendicular * p.x;
            float interceptX = (yIterceptPerpendicular - y_intercept) / (gradient - gradientPerpendicular);
            float interceptY = gradient * interceptX + y_intercept;
            return Vector2.Distance(p, new Vector2(interceptX, interceptY));
        }
        public void DrawWithGizmos(float length)
        {
            Vector3 lineDir = new Vector3(1, 0, gradient).normalized;
            Vector3 lineCentre = new Vector3(_pointOnLine_1.x, 0, _pointOnLine_2.y) + Vector3.up;
            Gizmos.DrawLine(lineCentre - lineDir * length / 2, lineCentre + lineDir * length / 2);
        }
    }
}
