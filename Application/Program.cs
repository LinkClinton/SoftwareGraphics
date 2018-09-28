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
    class Program
    {
        static void Main(string[] args)
        {
            GameScene.Main = new GameScene("SoftwaveGraphics", new Size(1280, 720));

            GameScene.Main.SetBehaviorSystem(new ImageRenderSystem(GameScene.Main));
            GameScene.Main.SetGameObject(new ImageObject(GameScene.Main));
            
            GalEngine.Application.Create("SoftwaveGraphics", new Size(1280, 720), null);
            GalEngine.Application.RunLoop();
        }
    }
}
