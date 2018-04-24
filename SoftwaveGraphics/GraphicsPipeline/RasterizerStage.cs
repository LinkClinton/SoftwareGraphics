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
        private CullMode cullMode = CullMode.None;

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

                    break;
                case FrustumFace.Right:
                    // (start.x + vector.x * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.x) / (vector.x - vector.w)
                    // and (vector.x - vector.w) != 0
                    // because there is most one vertex on the edge(x = w), so (vector.x - vector.w) = (end.x - start.x - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.X) /
                        (vector.PositionTransformed.X - vector.PositionTransformed.W);

                    break;
                case FrustumFace.Bottom:
                    // (start.y + vector.y * t) / (start.w + vector.w * t) = -1
                    // t = (start.y + start.w) / (vector.y + vector.w)
                    // and (vector.y + vector.w) != 0
                    // because there is most one vertex on the edge(y = -w), so (vector.y + vector.w) = (end.y - start.y + end.w - start.w) != 0

                    t = (start.PositionTransformed.Y + start.PositionTransformed.W) /
                        (vector.PositionTransformed.Y + vector.PositionTransformed.W);

                    break;
                case FrustumFace.Top:
                    // (start.y + vector.y * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.y) / (vector.y - vector.w)
                    // and (vector.y - vector.w) != 0
                    // because there is most one vertex on the egde(y = w), so (vector.y - vector.w) = (end.y - start.y - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.Y) /
                        (vector.PositionTransformed.Y - vector.PositionTransformed.W);

                    break;
                case FrustumFace.Near:
                    // (start.z + vector.z * t) / (start.w + vector.w * t) = 0
                    // t = - (start.z / vector.z)
                    // and vector.z != 0
                    // because there is most one vertex on the edge(z = 0), so vector.z = (end.z - start.z) != 0

                    t = -(start.PositionTransformed.Z / vector.PositionTransformed.Z);

                    break;
                case FrustumFace.Far:
                    // (start.z + vector.z * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.z) / (vector.z - vector.w) 
                    // and (vector.z - vector.w) != 0
                    // because there is most one vertex on the edge(z = w), so (vector.z - vector.w) = (end.z - start.z - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.Z) /
                        (vector.PositionTransformed.Z - vector.PositionTransformed.W);

                    break;
                default:
                    break;
            }

            var result = start + vector * t;

            //divide for the new vertex
            result.PositionAfterDivide = result.PositionTransformed / result.PositionTransformed.W;

            return result;
        }

        //Cull the primitives
        private void CullPrimitives(ref DrawCall drawCall)
        {
            //do not cull when CullMode is none or the primitive is not triangle(maybe line or point)
            if (cullMode is CullMode.None && drawCall.PrimitiveType != PrimitiveType.TriangleList) return;

            //result
            List<Primitive> primitives = new List<Primitive>();

            //for all primitives in the drawCall
            foreach (var item in drawCall.Primitives)
            {
                //these vertics are in the NDC space
                //so we only need discuss the x and y
                //because the x and y are in the same plane(project window)
                var u = item.Vertics[1].PositionAfterDivide - item.Vertics[0].PositionAfterDivide;
                var v = item.Vertics[2].PositionAfterDivide - item.Vertics[0].PositionAfterDivide;

                //only need discuss the x and y
                var projectU = new System.Numerics.Vector2(u.X, u.Y);
                var projectV = new System.Numerics.Vector2(v.X, v.Y);

                //cull
                switch (cullMode)
                {
                    case CullMode.None:
                        break;
                    case CullMode.Front:
                        //u cross v = u.x * v.y - u.y * v.x
                        //if u cross v < 0 it means the triangle is front-facing
                        if (MathHelper.Cross(projectU, projectV) < 0)
                            continue;

                        break;
                    case CullMode.Back:
                        //u cross v = u.x * v.y - u.y * v.x
                        //if u cross v < 0 it means the triangle is back-facing
                        if (MathHelper.Cross(projectU, projectV) > 0)
                            continue;
                        break;
                    default:
                        break;
                }

                //do not cull
                primitives.Add(item);
            }

            //result
            drawCall.Primitives = primitives.ToArray();
        }

        //Sutherland-Hodgeman algorithm
        private void ClipPrimitives(ref DrawCall drawCall)
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
            //first we cull the primitives
            CullPrimitives(ref drawCall);

            //second we clip the primitives and we use Sutherland-Hodgeman algorithm
            ClipPrimitives(ref drawCall);

        }

        public CullMode CullMode
        {
            set => cullMode = value;
            get => cullMode;
        }
    }

    class RasterizerStageInstance : RasterizerStage
    {
        public RasterizerStageInstance(GraphicsPipeline GraphicsPipeline) : base(GraphicsPipeline)
        {

        }
    }

}
