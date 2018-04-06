using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwaveGraphics
{
    enum FrustumFace : int
    {
        Left,
        Right,
        Bottom,
        Top,
        Near,
        Far
    }

    public class RasterizerStage : BaseStage
    {
        public RasterizerStage(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }

        private static UnitProperty ClipEdge(UnitProperty start, UnitProperty end, FrustumFace faceIndex)
        {
            float t = 0;

            // result = start + end * t
            switch (faceIndex)
            {
                case FrustumFace.Left:
                    // (start.x + end.x * t) / (start.w + end.w * t) = -1
                    // t = (start.x + start.w) / (end.x + end.w)
                    // and (end.x / end.w) != -1

                    t = (start.PositionTransformed.X + start.PositionTransformed.W) /
                        (end.PositionTransformed.X + end.PositionTransformed.W);

                    return start + end * t;
                case FrustumFace.Right:
                    // (start.x + end.x * t) / (start.w + end.w * t) = 1
                    // t = (start.w - start.x) / (end.x - end.w)
                    // and (end.x / end.w) != 1

                    t = (start.PositionTransformed.W - start.PositionTransformed.X) /
                        (end.PositionTransformed.X - end.PositionTransformed.W);

                    return start + end * t;

                case FrustumFace.Bottom:
                    // (start.y + end.y * t) / (start.w + end.w * t) = -1
                    // t = (start.y + start.w) / (end.y + end.w)
                    // and (end.y / end.w) != -1 

                    t = (start.PositionTransformed.Y + start.PositionTransformed.W) /
                        (end.PositionTransformed.Y + end.PositionTransformed.W);

                    return start + end * t;

                case FrustumFace.Top:
                    // (start.y + end.y * t) / (start.w + end.w * t) = 1
                    // t = (start.w - start.y) / (end.y - end.w)
                    // and (end.y /end.w) != 1

                    t = (start.PositionTransformed.W - start.PositionTransformed.Y) /
                        (end.PositionTransformed.Y - end.PositionTransformed.W);

                    return start + end * t;

                case FrustumFace.Near:
                    // (start.z + end.z * t) / (start.w + end.w * t) = 0
                    // t = - (start.z / end.z)
                    // and end.z != 0

                    t = -(start.PositionTransformed.Z / end.PositionTransformed.Z);

                    return start + end * t;

                case FrustumFace.Far:
                    // (start.z + end.z * t) / (start.w + end.w * t) = 1
                    // t = (start.w - start.z) / (end.z - end.w) 

                    t = (start.PositionTransformed.W - start.PositionTransformed.Z) /
                        (end.PositionTransformed.Z - end.PositionTransformed.W);

                    return start + end * t;
                default:
                    return start + end * t;
            }
        }

        //Sutherland-Hodgeman algorithm
        private void ClipAlgorithm(ref DrawCall drawCall)
        {
            //we enum all primitives in the draw call
            for (int i = 0; i < drawCall.Primitives.Length; i++)
            {
                //create a temp for calculating
                var result = new Primitive(drawCall.Primitives[i].Vertics);
                var verticesList = new List<UnitProperty>();

                //for all face of frustum
                for (FrustumFace face = FrustumFace.Left; face <= FrustumFace.Far; face++)
                {
                    verticesList.Clear();

                    //enum all edge in edge primitive
                    for (int vertexIndex = 0; vertexIndex < result.Vertics.Length; vertexIndex++)
                    {
                        var currentVertex = result.Vertics[vertexIndex];
                        var nextVertex = result.Vertics[0];

                        //the last edge
                        if (vertexIndex + 1 != result.Vertics.Length)
                            nextVertex = result.Vertics[vertexIndex + 1];



                        //the edge is not inside the clip boundary, so we do not add the vertex to result
                        if (currentVertex.IsInsideClipBoundary(face) is false
                            && nextVertex.IsInsideClipBoundary(face) is false)
                            continue;

                        //the edge is inside the clip boundary, so we add the vertex to result
                        if (currentVertex.IsInsideClipBoundary(face) is true
                            && nextVertex.IsInsideClipBoundary(face) is true)
                        {
                            //we only add the start vertex
                            verticesList.Add(currentVertex);

                            continue;
                        }

                        //for intersecting
                        //the start is not insided but the end is insided.
                        if (currentVertex.IsInsideClipBoundary(face) is false)
                        {
                            verticesList.Add(ClipEdge(currentVertex, nextVertex, face));
                            verticesList.Add(nextVertex);

                            continue;
                        }

                        //the start is insided but the end is not insided
                        if (nextVertex.IsInsideClipBoundary(face) is false)
                        {
                            verticesList.Add(currentVertex);
                            verticesList.Add(ClipEdge(currentVertex, nextVertex, face));

                            continue;
                        }
                    }

                    //update the primitive
                    result = new Primitive(verticesList.ToArray());
                }

                //get result
                drawCall.Primitives[i] = result;
            }
        }

        internal override void OnProcessStage(ref DrawCall drawCall)
        {
            //first we clip the primitives and we use Sutherland-Hodgeman algorithm
            ClipAlgorithm(ref drawCall);

        }
    }

    class RasterizerStageInstance : RasterizerStage
    {
        public RasterizerStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }

}
