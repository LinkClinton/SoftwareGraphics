using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class Texture2D<T> : Resource<T>
    {
        private int width;
        private int height;
        private int rowPitch;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public Texture2D(int Width, int Height) : base(Width * Height)
        {
            width = Width;
            height = Height;

            rowPitch = width * ElementSize;
        }

        public Texture2D(T[,] data = null) : this(data.GetLength(0), data.GetLength(1))
        {
            data.CopyTo(resource, 0);
        }

        public void SetValue(int x, int y, T value)
        {
            resource[width * y + x] = value;    
        }

        public T GetValue(int x,int y)
        {
            return resource[width * y + x];
        }
        
    }
}
