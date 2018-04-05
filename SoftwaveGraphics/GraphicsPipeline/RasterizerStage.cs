using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class RasterizerStage : BaseStage
    {
        public RasterizerStage(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }

        internal override void OnProcessStage(ref DrawCall drawCall)
        {

        }
    }

    class RasterizerStageInstance : RasterizerStage
    {
        public RasterizerStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }

}
