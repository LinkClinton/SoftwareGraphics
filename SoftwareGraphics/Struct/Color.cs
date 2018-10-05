using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGraphics
{
    public struct Color
    {
        private static Color black = new Color(0, 0, 0, 255);

        public byte Red;
        public byte Green;
        public byte Blue;
        public byte Alpha;

        public Color(byte red, byte green, byte blue, byte alpha)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public Color(System.Numerics.Vector4 color)
        {
            Red = (byte)(color.X * 255);
            Green = (byte)(color.Y * 255);
            Blue = (byte)(color.Z * 255);
            Alpha = (byte)(color.W * 255);
        }

        public static Color Black => black;
    }

}
