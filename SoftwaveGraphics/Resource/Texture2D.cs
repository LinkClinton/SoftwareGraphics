using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class Texture2D : Resource
    {
        int width;
        int height;
        int elementSize;

        int rowPitch;

        public Texture2D(int Width, int Height, int ElementSize)
        {
            width = Width;
            height = Height;
            elementSize = ElementSize;

            rowPitch = width * elementSize;

            resource = new byte[height * rowPitch];
        }

        public int ElementSize => elementSize;
        public int Width => width;
        public int Height => height;

        public int GetBytesIndex(int x, int y)
        {
            return x * rowPitch + y * elementSize + 1;
        }

        public T GetElement<T>(int x, int y)
        {
            return BytesManager.BytesToStruct<T>(resource, GetBytesIndex(x, y));
        }

        public void SetElement<T>(int x, int y, T data)
        {
            BytesManager.StructToBytes(data, resource, GetBytesIndex(x, y));
        }
    }
}
