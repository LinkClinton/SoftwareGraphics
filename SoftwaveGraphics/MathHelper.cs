using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SoftwaveGraphics
{
    public static class MathHelper
    {
        public static float Cross(Vector2 u, Vector2 v)
        {
            //u cross v = u.x * v.y - u.y * v.x
            return u.X * v.Y - u.Y * v.X;
        }
    }
}
