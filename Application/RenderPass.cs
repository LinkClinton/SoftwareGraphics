using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using SoftwareGraphics;

namespace Application
{
    public static class RenderPass
    {
        private static GraphicsPipeline graphicsPipeline;

        private static TestVertexShader vertexShader = new TestVertexShader();
        private static TestPixelShader pixelShader = new TestPixelShader();

        private static UnitProperty[] vertices;
        private static uint[] indices;
        private static object[] matrix;

        private static Cube cube;

        private static Camera camera = new Camera();

        private static void CreateBuffer()
        {
            vertices = new UnitProperty[3];


            vertices[0] = new UnitProperty();
            vertices[1] = new UnitProperty();
            vertices[2] = new UnitProperty();

            vertices[0].Position = new Vector3(0, 0, 0);
            vertices[1].Position = new Vector3(5, 0, 0);
            vertices[2].Position = new Vector3(0, 5, 0);

            vertices[0].Color = new Vector4(1, 0, 0, 1);
            vertices[1].Color = new Vector4(0, 1, 0, 1);
            vertices[2].Color = new Vector4(0, 0, 1, 1);

            
            indices = new uint[36] {
                0,1,2,0,2,3,4,6,5,4,7,6,
                4,5,1,4,1,0,3,2,6,3,6,7,
                1,5,6,1,6,2,4,0,3,4,3,7
            };

            cube = new Cube(3, 3, 3);

            camera.Position = new Vector3(0, 0, 10);
            camera.LookAt = new Vector3(0, 0, 0);

            matrix = new object[3];

            matrix[0] = Matrix4x4.CreateTranslation(new Vector3(0, 0, 0));
            matrix[1] = Matrix4x4.CreateLookAt(camera.Position, camera.LookAt, new Vector3(0, 1, 0));
            matrix[2] = Matrix4x4.CreatePerspectiveFieldOfView((float)Math.PI * 0.55f,
                    Program.Size.Width / (float)Program.Size.Height, 1.0f, 2000.0f);
        }

        static RenderPass()
        {
            graphicsPipeline = new GraphicsPipeline();

            CreateBuffer();
        }

        public static void Update(float deltaTime)
        {
            matrix[0] = (Matrix4x4)matrix[0] * Matrix4x4.CreateRotationY((float)Math.PI * deltaTime * 0.2f);
        }

        public static void Draw(RenderTarget renderTarget)
        {
            graphicsPipeline.InputAssemblerStage.Vertics = cube.Vertics;
            graphicsPipeline.InputAssemblerStage.Indices = cube.Indices;
            graphicsPipeline.InputAssemblerStage.InputData = matrix;

            graphicsPipeline.VertexShaderStage.VertexShader = vertexShader;
            graphicsPipeline.PixelShaderStage.PixelShader = pixelShader;

            graphicsPipeline.RasterizerStage.CullMode = CullMode.Front;

            graphicsPipeline.OutputMergerStage.RenderTarget = renderTarget;

            graphicsPipeline.Draw(36, 0, 0);
        }
        
        public static GraphicsPipeline GraphicsPipeline { get => graphicsPipeline; }
    }
}
