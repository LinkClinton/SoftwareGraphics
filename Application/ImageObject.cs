using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwaveGraphics;

using GalEngine;
using GalEngine.Extension;


namespace Application
{
    class ImageObject : GameObject
    {
        private GameScene gameScene;

        private Transform transform;
        private ImageShape imageShape;
        private Bitmap renderBuffer;

        private RenderTarget renderTarget;
        private GraphicsPipeline graphicsPipeline;

        private byte[] bytes;

        public ImageObject(GameScene gameScene)
        {
            this.gameScene = gameScene;

            this.SetComponent(transform = new Transform());
            this.SetComponent(imageShape = new ImageShape(gameScene.Resolution, "RenderBuffer"));

            this.renderBuffer = new Bitmap(gameScene.Resolution);

            this.renderTarget = new RenderTarget(gameScene.Resolution.Width, gameScene.Resolution.Height);
            this.graphicsPipeline = new GraphicsPipeline();

            this.bytes = new byte[renderTarget.SizeBytes];

            GameResource.SetBitmap("RenderBuffer", renderBuffer);
            //GameResource.SetBitmap("RenderBuffer", new System.IO.FileStream("Bitmap.png", System.IO.FileMode.Open));
        }

        public override void Update(float deltaTime)
        {
            renderTarget.Clear();

            //draw

            renderTarget.CopyTo(ref bytes);
            renderBuffer.CopyFromMemroy(bytes);

            base.Update(deltaTime);
        }
    }
}
