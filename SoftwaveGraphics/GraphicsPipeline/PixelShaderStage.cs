using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class PixelShaderStage : BaseStage
    {
        private PixelShader pixelShader = null;

        public PixelShaderStage(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }

        public PixelShader PixelShader { get => pixelShader; set => pixelShader = value; }

        internal override void OnProcessStage(ref DrawCall drawCall)
        {
            throw new NotImplementedException();
        }
    }

    class PixelShaderStageInstance : PixelShaderStage
    {
        public PixelShaderStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }
}
