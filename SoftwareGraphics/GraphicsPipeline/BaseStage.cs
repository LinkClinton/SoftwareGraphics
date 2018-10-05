using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGraphics
{
    public abstract class BaseStage
    {
        private GraphicsPipeline graphicsPipeline;

        internal abstract void OnProcessStage(ref DrawCall drawCall);

        public BaseStage(GraphicsPipeline GraphicsPipeline)
        {
            graphicsPipeline = GraphicsPipeline;
        }

        internal GraphicsPipeline GraphicsPipeline => graphicsPipeline;
    }
}
