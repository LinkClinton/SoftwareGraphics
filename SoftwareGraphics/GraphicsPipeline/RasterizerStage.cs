using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
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

        //Clip the edge with a face
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
                    // t = -(start.x + start.w) / (vector.x + vector.w)
                    // and (vector.x + vector.w) != 0 
                    // because there is most one vertex on the face(x = -w), so (vector.x + vector.w) = (end.x - start.x + end.w - start.w) != 0

                    t = -(start.PositionTransformed.X + start.PositionTransformed.W) /
                        (vector.PositionTransformed.X + vector.PositionTransformed.W);

                    break;
                case FrustumFace.Right:
                    // (start.x + vector.x * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.x) / (vector.x - vector.w)
                    // and (vector.x - vector.w) != 0
                    // because there is most one vertex on the face(x = w), so (vector.x - vector.w) = (end.x - start.x - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.X) /
                        (vector.PositionTransformed.X - vector.PositionTransformed.W);

                    break;
                case FrustumFace.Bottom:
                    // (start.y + vector.y * t) / (start.w + vector.w * t) = -1
                    // t = (start.y + start.w) / (vector.y + vector.w)
                    // and (vector.y + vector.w) != 0
                    // because there is most one vertex on the face(y = -w), so (vector.y + vector.w) = (end.y - start.y + end.w - start.w) != 0

                    t = -(start.PositionTransformed.Y + start.PositionTransformed.W) /
                        (vector.PositionTransformed.Y + vector.PositionTransformed.W);

                    break;
                case FrustumFace.Top:
                    // (start.y + vector.y * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.y) / (vector.y - vector.w)
                    // and (vector.y - vector.w) != 0
                    // because there is most one vertex on the face(y = w), so (vector.y - vector.w) = (end.y - start.y - end.w + start.w) != 0

                    t = (start.PositionTransformed.W - start.PositionTransformed.Y) /
                        (vector.PositionTransformed.Y - vector.PositionTransformed.W);

                    break;
                case FrustumFace.Near:
                    // (start.z + vector.z * t) / (start.w + vector.w * t) = 0
                    // t = - (start.z / vector.z)
                    // and vector.z != 0
                    // because there is most one vertex on the face(z = 0), so vector.z = (end.z - start.z) != 0

                    t = -(start.PositionTransformed.Z / vector.PositionTransformed.Z);

                    break;
                case FrustumFace.Far:
                    // (start.z + vector.z * t) / (start.w + vector.w * t) = 1
                    // t = (start.w - start.z) / (vector.z - vector.w) 
                    // and (vector.z - vector.w) != 0
                    // because there is most one vertex on the face(z = w), so (vector.z - vector.w) = (end.z - start.z - end.w + start.w) != 0

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
                var projectU = new Vector2(u.X, u.Y);
                var projectV = new Vector2(v.X, v.Y);

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
                
                //for all face of frustum
                for (FrustumFace face = FrustumFace.Left; face <= FrustumFace.Far; face++)
                {
                    var verticesList = new List<UnitProperty>();

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
                        
                        //the start is not insided but the end is insided.
                        if (currentVertex.IsInsideClipBoundary(face) is false)
                        {
                            verticesList.Add(ClipEdge(currentVertex, nextVertex, face));
                            
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

        //Divide primitives into triangles
        private void TriangulatePrimitives(ref DrawCall drawCall)
        {
            var primitives = new List<Primitive>();

            foreach (var item in drawCall.Primitives)
            {
                //ignore the primitive that was cliped
                if (item.Vertics.Length == 0) continue;

                //we select a vertex to make triangulate
                var importantVertex = item.Vertics[0];

                //we can know the number of triangles is (item.Vertics.Length - 2)
                for (int i = 0; i < item.Vertics.Length - 2; i++)
                {
                    var vertics = new UnitProperty[3];

                    //new triangle
                    vertics[0] = importantVertex;
                    vertics[1] = item.Vertics[i + 1];
                    vertics[2] = item.Vertics[i + 2];

                    //normalize the ordering(counter-clockwise)
                    Utility.NormalizeOrdering(ref vertics);

                    //add it
                    primitives.Add(new Primitive(vertics));
                }
            }

            drawCall.Primitives = primitives.ToArray();
        }

        private void RasterizerPrimitives(ref DrawCall drawCall)
        {
            List<PixelProperty> pixels = new List<PixelProperty>();

            var renderTarget = GraphicsPipeline.OutputMergerStage.RenderTarget;

            //for each primitives(triangles)
            //and the primitives are counter-clockwise.
            foreach (var primitive in drawCall.Primitives)
            {
                //bounding box = (left, top, right, bottom)
                Vector4 boundingBox = new Vector4(float.MaxValue, float.MaxValue, float.MinValue, float.MinValue);

                //create triangle
                Vector2[] triangle = new Vector2[3];

                int index = 0;

                //enum the vertex
                foreach (var vertex in primitive.Vertics)
                {
                    //compute the bounding box
                    boundingBox.X = Math.Min(boundingBox.X, vertex.PositionAfterDivide.X);
                    boundingBox.Y = Math.Min(boundingBox.Y, vertex.PositionAfterDivide.Y);
                    boundingBox.Z = Math.Max(boundingBox.Z, vertex.PositionAfterDivide.X);
                    boundingBox.W = Math.Max(boundingBox.W, vertex.PositionAfterDivide.Y);

                    //make triangle data and convert it to the render target
                    //the range of point in the ndc space is [-1 ,1]
                    //so the point in the screen is (point + 1) * size * 0.5f
                    triangle[index] = new Vector2(
                        (primitive.Vertics[index].PositionAfterDivide.X + 1) * renderTarget.Width * 0.5f,
                        (primitive.Vertics[index].PositionAfterDivide.Y + 1) * renderTarget.Height * 0.5f);

                    index++;
                }

                //convert the boundingBox to the render target
                boundingBox.X = (float)Math.Floor((boundingBox.X + 1) * renderTarget.Width * 0.5f);
                boundingBox.Y = (float)Math.Floor((boundingBox.Y + 1) * renderTarget.Height * 0.5f);
                boundingBox.Z = (float)Math.Ceiling((boundingBox.Z + 1) * renderTarget.Width * 0.5f);
                boundingBox.W = (float)Math.Ceiling((boundingBox.W + 1) * renderTarget.Height * 0.5f);

                //for the float
                boundingBox.X = Utility.Limit(boundingBox.X, 0, renderTarget.Width);
                boundingBox.Y = Utility.Limit(boundingBox.Y, 0, renderTarget.Height);
                boundingBox.Z = Utility.Limit(boundingBox.Z, 0, renderTarget.Width);
                boundingBox.W = Utility.Limit(boundingBox.W, 0, renderTarget.Height);

                //get the triangle's area
                //we do not need to divide 2
                float triangleArea = MathHelper.AreaFunction(triangle[0], triangle[1], triangle[2]);

                //make the UnitProperties for computing the pixel's unitProperty
                //rule: P = z * (P0 / z0 * e0 + P1 / z1 * e1 + P2 / z2 * e2)
                //e0 + e1 + e2 = 1
                var unitProperties = new UnitProperty[3];

                unitProperties[0] = primitive.Vertics[0] / primitive.Vertics[0].PositionTransformed.Z;
                unitProperties[1] = primitive.Vertics[1] / primitive.Vertics[1].PositionTransformed.Z;
                unitProperties[2] = primitive.Vertics[2] / primitive.Vertics[2].PositionTransformed.Z;

                //make the invert z to compute the pixel's invert z
                //rule: Z = 1 / (1 / z0 * e0 + 1 / z1 * e1 + 1 / z2 * e2)
                var triangleInvertZ = new float[3];

                triangleInvertZ[0] = 1.0f / primitive.Vertics[0].PositionTransformed.Z;
                triangleInvertZ[1] = 1.0f / primitive.Vertics[1].PositionTransformed.Z;
                triangleInvertZ[2] = 1.0f / primitive.Vertics[2].PositionTransformed.Z;

                //enum the pixel
                for (int x = (int)boundingBox.X; x <= boundingBox.Z; x++)
                {
                    for (int y = (int)boundingBox.Y; y <= boundingBox.W; y++)
                    {
                        //0.5f offset, because we will use the center of pixel
                        var pixel = new Vector2(x + 0.5f, y + 0.5f);

                        //compute the area(ratio) about sub-triangle
                        float subArea0 = MathHelper.AreaFunction(triangle[1], triangle[2], pixel) / triangleArea;
                        float subArea1 = MathHelper.AreaFunction(triangle[2], triangle[0], pixel) / triangleArea;
                        float subArea2 = MathHelper.AreaFunction(triangle[0], triangle[1], pixel) / triangleArea;

                        if (subArea0 < 0 || subArea1 < 0 || subArea2 < 0) continue;

                        //compute the property
                        UnitProperty pixelProperty 
                            = unitProperties[0] * subArea0 + unitProperties[1] * subArea1 + unitProperties[2] * subArea2;

                        float invertZ = 1.0f / (triangleInvertZ[0] * subArea0 + triangleInvertZ[1] * subArea1 + triangleInvertZ[2] * subArea2);

                        pixelProperty = pixelProperty * invertZ;

                        //accept the pixel
                        pixels.Add(new PixelProperty(pixelProperty, new Vector2(x, y)));
                    }
                }
            }

            //get the pixels
            drawCall.Pixels = pixels.ToArray();
        }

        internal override void OnProcessStage(ref DrawCall drawCall)
        {
            //first we cull the primitives
            CullPrimitives(ref drawCall);

            //second we clip the primitives and we use Sutherland-Hodgeman algorithm
            ClipPrimitives(ref drawCall);

            //third we divide primitives into triangles
            TriangulatePrimitives(ref drawCall);

            //at last, we do the rasterization
            RasterizerPrimitives(ref drawCall);
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
