using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareGraphics;

using GalEngine;
using GalEngine.Extension;


namespace Application
{
    public class ImageObject : GameObject
    {
        private GameScene _gameScene;

        private Transform _transform;
        private ImageShape _imageShape;
        private readonly Bitmap _renderBuffer;

        private readonly RenderTarget _renderTarget;
        
        private byte[] _bytes;

        public ImageObject(GameScene gameScene)
        {
            this._gameScene = gameScene;

            this.SetComponent(_transform = new Transform());
            this.SetComponent(_imageShape = new ImageShape(gameScene.Resolution, "RenderBuffer"));

            this._renderBuffer = new Bitmap(gameScene.Resolution);

            this._renderTarget = new RenderTarget(gameScene.Resolution.Width, gameScene.Resolution.Height);
            
            this._bytes = new byte[_renderTarget.SizeBytes];

            GameResource.SetBitmap("RenderBuffer", _renderBuffer);
        }

        public override void Update(float deltaTime)
        {
            RenderPass.Update(deltaTime);

            _renderTarget.Clear();

            //draw
            RenderPass.Draw(_renderTarget);

            _renderTarget.CopyTo(ref _bytes);
            _renderBuffer.CopyFromMemroy(_bytes);

            base.Update(deltaTime);
        }
    }
}
