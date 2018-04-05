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
            //first we clip the primitives and we use Sutherland-Hodgeman algorithm
            //for every primitive
            for (int i = 0; i < drawCall.Primitives.Length; i++)
            {
                
            }

        }
    }

    class RasterizerStageInstance : RasterizerStage
    {
        public RasterizerStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }

}
