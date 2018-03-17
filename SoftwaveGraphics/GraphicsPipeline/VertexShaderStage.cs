using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class VertexShaderStage : BaseStage
    {
        private VertexShader vertexShader = null;

        public VertexShader VertexShader { get => vertexShader; set => vertexShader = value; }

        internal override void OnProcessStage()
        {
            throw new NotImplementedException();
        }
    }
}
