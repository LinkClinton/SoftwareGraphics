using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public static class Utility
    {
        //true is counter-clockwise and false is clockwise 
        public static bool GetTriangleOrdering(Vector2[] triangle)
        {
            Vector2 u = triangle[1] - triangle[0];
            Vector2 v = triangle[2] - triangle[0];

            return MathHelper.Cross(u, v) > 0;
        }

        public static bool GetTriangleOrdering(UnitProperty[] triangle)
        {
            Vector4 u = triangle[1].PositionAfterDivide - triangle[0].PositionAfterDivide;
            Vector4 v = triangle[2].PositionAfterDivide - triangle[0].PositionAfterDivide;

            return MathHelper.Cross(new Vector2(u.X, u.Y), new Vector2(v.X, v.Y)) > 0;
        }

        //change the ordering of triangle into counter-clockwise
        public static void NormalizeOrdering(ref Vector2[] triangle)
        {
            if (GetTriangleOrdering(triangle) is true) return;

            var tempVertex1 = triangle[1];
            var tempVertex2 = triangle[2];

            triangle[1] = tempVertex2;
            triangle[2] = tempVertex1;
        }

        public static void NormalizeOrdering(ref UnitProperty[] triangle)
        {
            if (GetTriangleOrdering(triangle) is true) return;

            var tempVertex1 = triangle[1];
            var tempVertex2 = triangle[2];

            triangle[1] = tempVertex2;
            triangle[2] = tempVertex1;
        }

        public static bool IsTriangleContained(Vector2 point, params Vector2[] triangle)
        {
            //get the ordering
            bool ordering = GetTriangleOrdering(triangle);

            //edges of triangle
            Vector2[] edges = new Vector2[3];

            edges[0] = triangle[1] - triangle[0];
            edges[1] = triangle[2] - triangle[1];
            edges[2] = triangle[0] - triangle[2];

            //the point transform
            Vector2[] testPoints = new Vector2[3];

            testPoints[0] = point - triangle[0];
            testPoints[1] = point - triangle[1];
            testPoints[2] = point - triangle[2];

            for (int i = 0; i < 3; i++)
            {
                //true: point is at the left of edges[i].
                bool result = MathHelper.Cross(edges[i], testPoints[i]) > 0;

                if (result != ordering) return false;
            }

            return true;
        }

        public static float ComputeTriangleArea(Vector2[] triangle)
        {
            return Math.Abs(MathHelper.Cross(triangle[1] - triangle[0], triangle[2] - triangle[0])) * 0.5f;
        }

        
    }
}
