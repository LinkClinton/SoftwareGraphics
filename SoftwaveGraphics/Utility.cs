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

        public static bool IsTriangleContained(Vector2 point, Vector2[] triangle)
        {
            //get the ordering
            bool ordering = GetTriangleOrdering(triangle);

            //edges of triangle
            Vector2[] edges = new Vector2[3]
            {
                triangle[1] - triangle[0],
                triangle[2] - triangle[1],
                triangle[0] - triangle[2]
            };

            //the point transform
            Vector2[] testPoints = new Vector2[3]
            {
                point - triangle[0],
                point - triangle[1],
                point - triangle[2]
            };

            for (int i = 0; i < 3; i++)
            {
                //true: point is at the left of edges[i].
                bool result = MathHelper.Cross(edges[i], testPoints[i]) > 0;

                if (result != ordering) return false;
            }

            return true;
        }
    }
}
