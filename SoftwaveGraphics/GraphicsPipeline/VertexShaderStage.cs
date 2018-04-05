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
            var maxVertexIndex = 0;
            var baseVertexLocation = drawCall.BaseVertexLocation;

            //Now we only have TriangleList Type
            drawCall.Primitives = new Primitive[drawCall.IndexCount / 3];

            //get buffer data
            var vertics = GraphicsPipeline.InputAssemblerStage.Vertics as UnitProperty[];
            var indices = GraphicsPipeline.InputAssemblerStage.Indices as int[];

            //for all primitives that we want to draw
            for (int index = drawCall.StartIndexLocation; index <= drawCall.EndIndexLocation; index += 3)
            {
                //create primitives
                drawCall.Primitives[index / 3] = new Primitive(
                    vertics[baseVertexLocation + indices[index]],
                    vertics[baseVertexLocation + indices[index + 1]],
                    vertics[baseVertexLocation + indices[index + 2]]);

                //find the range of vertices (from baseVertexLocation to baseVertexLocation + maxVertexIndex)
                //but I can not make sure the DirectX and Vulkan how to do this.
                maxVertexIndex = Math.Max(maxVertexIndex, indices[index]);
                maxVertexIndex = Math.Max(maxVertexIndex, indices[index + 1]);
                maxVertexIndex = Math.Max(maxVertexIndex, indices[index + 2]);
            }

            //create the array of result 
            drawCall.VertexResultProperties = new UnitProperty[maxVertexIndex];

            //the end position of vertex
            var endVertexLocation = baseVertexLocation + maxVertexIndex;

            //run vertex shader for every vertex
            for (int vertexIndex = baseVertexLocation; vertexIndex <= endVertexLocation; vertexIndex++) 
            {
                //create result
                UnitProperty result = new UnitProperty();

                //run vertex shader
                vertexShader.StartProcessUnit(ref result, vertics[vertexIndex],
                    GraphicsPipeline.InputAssemblerStage.InputData);

                //get the result
                drawCall.VertexResultProperties[vertexIndex - baseVertexLocation] = result;
            }
        }
    }

    class VertexShaderStageInstance : VertexShaderStage
    {
        public VertexShaderStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }
}
