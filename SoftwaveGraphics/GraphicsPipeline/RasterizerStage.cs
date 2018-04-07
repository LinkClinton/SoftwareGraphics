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

            //vector = end - start
            var vector = end - start;

            // result = start + (end - start) * t
            switch (faceIndex)
            {
                case FrustumFace.Left:
                    // (start.x + vector.x * t) / (start.w + vector.w * t) = -1
                    // t = (start.x + start.w) / (vector.x + vector.w)
                    // and (vector.x + vector.w) != 0 
                    // because there is most one vertex on the edge(x = -w), so (vector.x + vector.w) = (end.x - start.x + end.w - start.w) != 0

                    t = (start.PositionTransformed.X + start.PositionTransformed.W) /
                        (vector.PositionTransformed.X + vector.PositionTransformed.W);

                    return start + vector * t;
                case FrustumFace.Right:
                    // (start.x + vector.x * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.x) / (vector.x - vector.w)
                    // and (vector.x - vector.w) != 0
                    // because there is most one vertex on the edge(x = w), so (vector.x - vector.w) = (end.x - start.x - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.X) /
                        (vector.PositionTransformed.X - vector.PositionTransformed.W);

                    return start + vector * t;

                case FrustumFace.Bottom:
                    // (start.y + vector.y * t) / (start.w + vector.w * t) = -1
                    // t = (start.y + start.w) / (vector.y + vector.w)
                    // and (vector.y + vector.w) != 0
                    // because there is most one vertex on the edge(y = -w), so (vector.y + vector.w) = (end.y - start.y + end.w - start.w) != 0

                    t = (start.PositionTransformed.Y + start.PositionTransformed.W) /
                        (vector.PositionTransformed.Y + vector.PositionTransformed.W);

                    return start + vector * t;

                case FrustumFace.Top:
                    // (start.y + vector.y * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.y) / (vector.y - vector.w)
                    // and (vector.y - vector.w) != 0
                    // because there is most one vertex on the egde(y = w), so (vector.y - vector.w) = (end.y - start.y - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.Y) /
                        (vector.PositionTransformed.Y - vector.PositionTransformed.W);

                    return start + vector * t;

                case FrustumFace.Near:
                    // (start.z + vector.z * t) / (start.w + vector.w * t) = 0
                    // t = - (start.z / vector.z)
                    // and vector.z != 0
                    // because there is most one vertex on the edge(z = 0), so vector.z = (end.z - start.z) != 0

                    t = -(start.PositionTransformed.Z / vector.PositionTransformed.Z);

                    return start + vector * t;

                case FrustumFace.Far:
                    // (start.z + vector.z * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.z) / (vector.z - vector.w) 
                    // and (vector.z - vector.w) != 0
                    // because there is most one vertex on the edge(z = w), so (vector.z - vector.w) = (end.z - start.z - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.Z) /
                        (vector.PositionTransformed.Z - vector.PositionTransformed.W);

                    return start + vector * t;
                default:
                    return start + vector * t;
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
