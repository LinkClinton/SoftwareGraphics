using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class GraphicsPipeline
    {
        private InputAssemblerStage inputAssemblerStage = new InputAssemblerStage();
        private VertexShaderStage vertexShaderStage = new VertexShaderStage();
        private RasterizerStage rasterizerStage = new RasterizerStage();
        private PixelShaderStage pixelShaderStage = new PixelShaderStage();
        private OutputMergerStage outputMergerStage = new OutputMergerStage();


        public InputAssemblerStage InputAssemblerStage { get => inputAssemblerStage; set => inputAssemblerStage = value; }
        public VertexShaderStage VertexShaderStage { get => vertexShaderStage; set => vertexShaderStage = value; }
        public RasterizerStage RasterizerStage { get => rasterizerStage; set => rasterizerStage = value; }
        public PixelShaderStage PixelShaderStage { get => pixelShaderStage; set => pixelShaderStage = value; }
        public OutputMergerStage OutputMergerStage { get => outputMergerStage; set => outputMergerStage = value; }
    }
}
