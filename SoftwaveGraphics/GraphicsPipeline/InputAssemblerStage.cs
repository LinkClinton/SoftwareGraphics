using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public abstract class InputAssemblerStage : BaseStage 
    {
        private Array vertics = null;
        private Array indices = null;
        private object[] inputData = null;

        public InputAssemblerStage(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }

        public Array Vertics { get => vertics; set => vertics = value; }
        public Array Indices { get => indices; set => indices = value; }
        public object[] InputData { get => inputData; set => inputData = value; }
        
        internal override void OnProcessStage(ref DrawCall drawCall)
        {
            
        }
    }

    class InputAssemblerStageInstance : InputAssemblerStage
    {
        public InputAssemblerStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }
}
