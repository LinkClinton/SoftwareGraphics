using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

using SoftwaveGraphics;

namespace Application
{
    public class TestVertexShader : VertexShader
    {
        protected override void OnProcessUnit(ref UnitProperty unitProperty, object vertex, params object[] inputData)
        {
            var node = (vertex as UnitProperty);

            var worldMatrix = (Matrix4x4)inputData[0];
            var cameraMatrix = (Matrix4x4)inputData[1];
            var projectMatrix = (Matrix4x4)inputData[2];

            unitProperty.Color = node.Color;
            unitProperty.Position = node.Position;
            unitProperty.PositionTransformed = new Vector4(node.Position, 1.0f);

            unitProperty.PositionTransformed = Vector4.Transform(unitProperty.PositionTransformed, worldMatrix);
            unitProperty.PositionTransformed = Vector4.Transform(unitProperty.PositionTransformed, cameraMatrix);
            unitProperty.PositionTransformed = Vector4.Transform(unitProperty.PositionTransformed, projectMatrix);

        }
    }
}
