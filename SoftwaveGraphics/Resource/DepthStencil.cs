using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SoftwaveGraphics
{
    class DepthStencil : Texture2D<float>
    {
        public DepthStencil(int Width, int Height) : base(Width, Height)
        {

        }
    }
}
