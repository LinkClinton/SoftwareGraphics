using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGraphics
{
    public class GraphicsPipeline
    {
        private InputAssemblerStage inputAssemblerStage = null;
        private VertexShaderStage vertexShaderStage = null;
        private RasterizerStage rasterizerStage = null;
        private PixelShaderStage pixelShaderStage = null;
        private OutputMergerStage outputMergerStage = null;

        public GraphicsPipeline()
        {
            inputAssemblerStage = new InputAssemblerStageInstance(this);
            vertexShaderStage = new VertexShaderStageInstance(this);
            rasterizerStage = new RasterizerStageInstance(this);
            pixelShaderStage = new PixelShaderStage(this);
            outputMergerStage = new OutputMergerStageInstance(this);
        }

        public void Draw(int indexCount = 0, int startIndexLocation = 0, int baseVertexLocation = 0)
        {
            DrawCall drawCall = new DrawCall(indexCount, startIndexLocation, baseVertexLocation, PrimitiveType.TriangleList);

            inputAssemblerStage.OnProcessStage(ref drawCall);

            vertexShaderStage.OnProcessStage(ref drawCall);

            rasterizerStage.OnProcessStage(ref drawCall);

            pixelShaderStage.OnProcessStage(ref drawCall);

            outputMergerStage.OnProcessStage(ref drawCall);
        }

        public InputAssemblerStage InputAssemblerStage { get => inputAssemblerStage; }
        public VertexShaderStage VertexShaderStage { get => vertexShaderStage; }
        public RasterizerStage RasterizerStage { get => rasterizerStage; }
        public PixelShaderStage PixelShaderStage { get => pixelShaderStage; }
        public OutputMergerStage OutputMergerStage { get => outputMergerStage; }
    }
}
