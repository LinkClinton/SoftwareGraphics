using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    class PixelProperty : UnitProperty
    {
        private Vector2 location = Vector2.Zero;

        public PixelProperty(UnitProperty baseProperty, Vector2 location)
        {
            Depth = baseProperty.Depth;
            Position = baseProperty.Position;
            PositionTransformed = baseProperty.PositionTransformed;
            PositionAfterDivide = baseProperty.PositionAfterDivide;
            Color = baseProperty.Color;

            Location = location;
        }

        public Vector2 Location { internal set => location = value; get => location; }
    }
}
