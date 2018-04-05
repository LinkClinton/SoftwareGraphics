using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class OutputMergerStage : BaseStage
    {
        public OutputMergerStage(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }

        internal override void OnProcessStage(ref DrawCall drawCall)
        {
            throw new NotImplementedException();
        }
    }

    class OutputMergerStageInstance : OutputMergerStage
    {
        public OutputMergerStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }
}
