using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class DrawCall
    {
        private int indexCount = 0;
        private int startIndexLocation = 0;
        private int baseVertexLocation = 0;

        private PrimitiveType primitiveType = PrimitiveType.Unknown;

        private UnitProperty[] vertexResultProperty = null;

        public DrawCall(int IndexCount = 0, int StartIndexLocation = 0,
            int BaseVertexLocation = 0, PrimitiveType PrimitiveType = PrimitiveType.TriangleList)
        {
            indexCount = IndexCount;
            startIndexLocation = StartIndexLocation;
            baseVertexLocation = BaseVertexLocation;

            primitiveType = PrimitiveType;
        }

        internal UnitProperty[] VertexResultProperty { get => vertexResultProperty; set => vertexResultProperty = value; }

        public int IndexCount => indexCount;
        public int StartIndexLocation => startIndexLocation;
        public int BaseVertexLocation => baseVertexLocation;
        public PrimitiveType PrimitiveType => primitiveType;
    }
}
