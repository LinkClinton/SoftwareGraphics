using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using SoftwaveGraphics;

namespace Application
{
    public class Cube
    {
        private static Random random = new Random();

        private UnitProperty[] vertics;
        private uint[] indices;

        public Cube(float width, float height, float depth)
        {
            vertics = new UnitProperty[8];

            for (int i = 0; i < vertics.Length; i++)
            {
                vertics[i] = new UnitProperty();
                vertics[i].Color = new Vector4((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble(), 1.0f);
            }

            float halfwidth = 0.5f * width;
            float halfheight = 0.5f * height;
            float halfdepth = 0.5f * depth;

            vertics[0].Position = new Vector3(-halfwidth, -halfheight, -halfdepth);
            vertics[1].Position = new Vector3(-halfwidth, halfheight, -halfdepth);
            vertics[2].Position = new Vector3(halfwidth, halfheight, -halfdepth);
            vertics[3].Position = new Vector3(halfwidth, -halfheight, -halfdepth);
            vertics[4].Position = new Vector3(-halfwidth, -halfheight, halfdepth);
            vertics[5].Position = new Vector3(-halfwidth, halfheight, halfdepth);
            vertics[6].Position = new Vector3(halfwidth, halfheight, halfdepth);
            vertics[7].Position = new Vector3(halfwidth, -halfheight, halfdepth);

            indices = new uint[36] {
                0,1,2,0,2,3,4,6,5,4,7,6,
                4,5,1,4,1,0,3,2,6,3,6,7,
                1,5,6,1,6,2,4,0,3,4,3,7
            };
        }

        public UnitProperty[] Vertics { get => vertics; }
        public uint[] Indices { get => indices; }
    }
}
