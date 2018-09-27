using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    class PixelProperty
    {
        private UnitProperty unitProperty = null;
        private Vector2 location = Vector2.Zero;

        public PixelProperty(UnitProperty baseProperty, Vector2 location)
        {
            unitProperty = baseProperty;

            Location = location;
        }

        public Vector2 Location { internal set => location = value; get => location; }
        public UnitProperty UnitProperty { internal set => unitProperty = value; get => unitProperty; }
    }
}
