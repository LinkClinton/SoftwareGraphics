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
        
        private byte[] bytes;

        public ImageObject(GameScene gameScene)
        {
            this.gameScene = gameScene;

            this.SetComponent(transform = new Transform());
            this.SetComponent(imageShape = new ImageShape(gameScene.Resolution, "RenderBuffer"));

            this.renderBuffer = new Bitmap(gameScene.Resolution);

            this.renderTarget = new RenderTarget(gameScene.Resolution.Width, gameScene.Resolution.Height);
            
            this.bytes = new byte[renderTarget.SizeBytes];

            GameResource.SetBitmap("RenderBuffer", renderBuffer);
        }

        public override void Update(float deltaTime)
        {
            RenderPass.Update(deltaTime);

            renderTarget.Clear();

            //draw
            RenderPass.Draw(renderTarget);

            renderTarget.CopyTo(ref bytes);
            renderBuffer.CopyFromMemroy(bytes);

            base.Update(deltaTime);
        }
    }
}
