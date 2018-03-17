using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class InputAssemblerStage : BaseStage 
    {
        private object vertics = null;
        private object indices = null;

        private PrimitiveType primitiveType = PrimitiveType.TriangleList;

        public object Vertics { get => vertics; set => vertics = value; }
        public object Indices { get => indices; set => indices = value; }
        public PrimitiveType PrimitiveType { get => primitiveType; set => primitiveType = value; }

        internal override void OnProcessStage()
        {
            throw new NotImplementedException();
        }
    }
}
