using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SoftwaveGraphics
{
    public class RenderTarget<T> : Texture2D
    {
        public RenderTarget(int Width, int Height) : base(Width, Height, Marshal.SizeOf<T>())
        {
        }
    }
}
