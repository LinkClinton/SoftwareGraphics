using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    public class UnitProperty
    {
        private float depth = float.NaN;

        private Vector3 position = Vector3.Zero;
        private Vector4 positionTransformed = Vector4.Zero;
        
        private Vector4 color = Vector4.Zero;

        public Vector3 Position { get => position; set => position = value; }
        public Vector4 PositionTransformed { get => positionTransformed; set => positionTransformed = value; }
        public Vector4 Color { get => color; set => color = value; }
        public float Depth { get => depth; set => depth = value; }
    }
}
