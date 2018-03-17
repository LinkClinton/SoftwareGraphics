using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class Resource
    {
        protected byte[] resource;

        protected Resource()
        {

        }

        public Resource(uint size)
        {
            resource = new byte[size];
        }

        public Resource(byte[] data)
        {
            resource = new byte[data.Length];

            data.CopyTo(resource, 0);
        }

        public byte[] Source { get => resource; set => resource = value; }

        public int Size => resource.Length;
    }
}
