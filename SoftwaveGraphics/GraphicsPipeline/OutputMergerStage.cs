using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class OutputMergerStage : BaseStage
    {
        private RenderTarget renderTarget = null;

        public OutputMergerStage(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }

        //procude the image
        private void RenderPixels(ref DrawCall drawCall)
        {
            //enum the pixel and set value
            foreach (var item in drawCall.Pixels)
            {
                renderTarget.SetValue((int)item.Location.X, (int)item.Location.Y,
                    item.UnitProperty.Color);
            }
        }

        internal override void OnProcessStage(ref DrawCall drawCall)
        {
            RenderPixels(ref drawCall);   
        }

        public RenderTarget RenderTarget { get => renderTarget; set => renderTarget = value; }
    }

    class OutputMergerStageInstance : OutputMergerStage
    {
        public OutputMergerStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }
}
