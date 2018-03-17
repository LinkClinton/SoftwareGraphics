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

        public PixelShader PixelShader { get => pixelShader; set => pixelShader = value; }

        internal override void OnProcessStage()
        {
            throw new NotImplementedException();
        }
    }
}
