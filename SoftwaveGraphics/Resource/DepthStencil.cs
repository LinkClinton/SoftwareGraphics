using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SoftwaveGraphics
{
    class DepthStencil<T> : Texture2D<T>
    {
        public DepthStencil(int Width, int Height) : base(Width, Height)
        {

        }
    }
}
