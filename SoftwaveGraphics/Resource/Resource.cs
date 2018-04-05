using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class Resource<T>
    {
        private int size;
        private int elementSize;
        private int sizeBytes;

        protected T[] resource;

        public Resource(int Size)
        {
            size = Size;
            elementSize = Marshal.SizeOf<T>();
            sizeBytes = size * elementSize;

            resource = new T[size];
        }

        public Resource(T[] data = null) : this(data.Length)
        {
            data.CopyTo(resource, 0);
        }

        public void SetElement(int position, T value)
        {
            resource[position] = value;
        }

        public T GetElement(int position)
        {
            return resource[position]; 
        }

        public void CopyFromBytes(byte[] data)
        {
            //Debug data's size.

            resource = BytesManager.BytesToStruct<T[]>(data);
        }

        public byte[] CopyToBytes()
        {
            return BytesManager.StructToBytes(resource);
        }

        public T[] Source => resource;

        public int Size => size;
        public int ElementSize => elementSize;
        public int SizeBytes => sizeBytes;
    }
}
