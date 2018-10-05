using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftwareGraphics
{
    public static class MathHelper
    {
        public static float Cross(Vector2 u, Vector2 v)
        {
            //u cross v = u.x * v.y - u.y * v.x
            return u.X * v.Y - u.Y * v.X;
        }

        //counter-clockwise, return the area of parallelogram
        public static float AreaFunction(Vector2 u, Vector2 v, Vector2 p)
        {
            return Cross(v - u, p - u);
        }
    }
}
