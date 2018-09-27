using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class DrawCall
    {
        private int indexCount = 0;
        private int startIndexLocation = 0;
        private int baseVertexLocation = 0;
        private int endIndexLocation = 0;

        private PrimitiveType primitiveType = PrimitiveType.Unknown;

        private UnitProperty[] vertexResultProperties = null;
        private PixelProperty[] pixels = null;
        private Primitive[] primitives = null;

        public DrawCall(int IndexCount = 0, int StartIndexLocation = 0,
            int BaseVertexLocation = 0, PrimitiveType PrimitiveType = PrimitiveType.TriangleList)
        {
            indexCount = IndexCount;
            startIndexLocation = StartIndexLocation;
            baseVertexLocation = BaseVertexLocation;
            endIndexLocation = startIndexLocation + indexCount - 1;

            primitiveType = PrimitiveType;
        }

        internal UnitProperty[] VertexResultProperties { get => vertexResultProperties; set => vertexResultProperties = value; }
        internal PixelProperty[] Pixels { get => pixels; set => pixels = value; }
        internal Primitive[] Primitives { get => primitives; set => primitives = value; }

        public int IndexCount => indexCount;
        public int StartIndexLocation => startIndexLocation;
        public int BaseVertexLocation => baseVertexLocation;
        public int EndIndexLocation => endIndexLocation;
        public PrimitiveType PrimitiveType => primitiveType;
    }
}
