using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public abstract class BaseStage
    {
        private GraphicsPipeline graphicsPipeline;

        internal abstract void OnProcessStage(ref DrawCall drawCall);

        public BaseStage(GraphicsPipeline GraphicsPipeline)
        {
            graphicsPipeline = GraphicsPipeline;
        }

        public GraphicsPipeline GraphicsPipeline => graphicsPipeline;
    }
}
