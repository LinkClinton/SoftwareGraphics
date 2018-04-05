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

        public VertexShaderStage(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {
            
        }

        public VertexShader VertexShader { get => vertexShader; set => vertexShader = value; }

        internal override void OnProcessStage(ref DrawCall drawCall)
        {

        }
    }

    class VertexShaderStageInstance : VertexShaderStage
    {
        public VertexShaderStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }
}
