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
        private Vector4 positionAfterDivide = Vector4.Zero;

        private Vector4 color = Vector4.Zero;

        internal bool IsInsideClipBoundary(FrustumFace face)
        {
            switch (face)
            {
                case FrustumFace.Left: return IsInsideLeftClipBoundary;
                case FrustumFace.Right: return IsInsideRightClipBoundary;
                case FrustumFace.Bottom: return IsInsideBottomClipBoundary;
                case FrustumFace.Top: return IsInsideTopClipBoundary;
                case FrustumFace.Near: return IsInsideNearClipBoundary;
                case FrustumFace.Far: return IsInsideFarClipBoundary;
                default:
                    return false;
            }
        }
        
        //left: x = -w
        public bool IsInsideLeftClipBoundary => positionTransformed.X >= -positionTransformed.W;
        //right: x = w
        public bool IsInsideRightClipBoundary => positionTransformed.X <= positionTransformed.W;
        //bottom: y = -w
        public bool IsInsideBottomClipBoundary => positionTransformed.Y >= -positionTransformed.W;
        //top: y = w
        public bool IsInsideTopClipBoundary => positionTransformed.Y <= positionTransformed.W;
        //near: z = 0
        public bool IsInsideNearClipBoundary => positionTransformed.Z >= 0;
        //far: z = w
        public bool IsInsideFarClipBoundary => positionTransformed.Z <= PositionTransformed.W;

        public Vector3 Position { get => position; set => position = value; }
        public Vector4 PositionTransformed { get => positionTransformed; set => positionTransformed = value; }
        public Vector4 PositionAfterDivide { get => positionAfterDivide; set => positionAfterDivide = value; }
        public Vector4 Color { get => color; set => color = value; }
        public float Depth { get => depth; set => depth = value; }

        public static UnitProperty Lerp(UnitProperty start, UnitProperty end, float scale)
        {
            return (end - start) * scale + start;
        }

        public static UnitProperty operator +(UnitProperty left, UnitProperty right)
        {
            var result = new UnitProperty()
            {
                color = right.color + left.color,
                depth = right.depth + left.depth,
                position = right.position + left.position,
                positionTransformed = right.positionTransformed + left.positionTransformed
            };

            result.positionAfterDivide = result.positionTransformed / result.positionTransformed.W;

            return result;
        }

        public static UnitProperty operator -(UnitProperty left, UnitProperty right)
        {
            var result=  new UnitProperty()
            {
                color = right.color - left.color,
                depth = right.depth - left.depth,
                position = right.position - left.position,
                positionTransformed = right.positionTransformed - left.positionTransformed
            };

            result.positionAfterDivide = result.positionTransformed / result.positionTransformed.W;

            return result;
        }

        public static UnitProperty operator *(UnitProperty left, float right)
        {
            var result = new UnitProperty()
            {
                color = left.color * right,
                depth = left.depth * right,
                position = left.position * right,
                positionTransformed = left.positionTransformed * right
            };

            result.positionAfterDivide = result.positionTransformed / result.positionTransformed.W;

            return result;
        }

        public static UnitProperty operator /(UnitProperty left, float right)
        {
            var result=  new UnitProperty()
            {
                color = left.color / right,
                depth = left.depth / right,
                position = left.position / right,
                positionTransformed = left.positionTransformed / right,
            };

            result.positionAfterDivide = result.positionTransformed / result.positionTransformed.W;

            return result;
        }
    }
}
