using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SoftwaveGraphics
{
    public class RenderTarget : Texture2D<Color>
    {
        public RenderTarget(int Width, int Height) : base(Width, Height)
        {
            
        }  

        public void Clear()
        {
            for (int i = 0; i < resource.Length; i++)
                resource[i] = Color.Black;
        }

        public byte[] CopyTo()
        {
            byte[] result = new byte[SizeBytes];

            for (int i = 0; i < SizeBytes; i += 4)
            {
                int index = i / 4;

                result[i] = resource[index].Blue;
                result[i + 1] = resource[index].Green;
                result[i + 2] = resource[index].Red;
                result[i + 3] = resource[index].Alpha;
            }

            return result;
        }

        public void CopyTo(ref byte[] bytes)
        {
            for (int i = 0; i < SizeBytes; i += 4)
            {
                int index = i / 4;

                bytes[i] = resource[index].Blue;
                bytes[i + 1] = resource[index].Green;
                bytes[i + 2] = resource[index].Red;
                bytes[i + 3] = resource[index].Alpha;
            }
        }
    }
}
