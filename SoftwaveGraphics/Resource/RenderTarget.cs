using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SoftwaveGraphics
{
    public class RenderTarget : Texture2D<Vector4>
    {
        public RenderTarget(int Width, int Height) : base(Width, Height)
        {

        }  
    }
}
