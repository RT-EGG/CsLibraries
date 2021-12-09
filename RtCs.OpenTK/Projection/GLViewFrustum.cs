using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    public class GLViewFrustum
    {
        public enum EVertex
        { 
            LeftBottomNear,
            LeftBottomFar,
            LeftTopNear,
            LeftTopFar,
            RightBottomNear,
            RightBottomFar,
            RightTopNear,
            RightTopFar,
        }

        public enum EEdge
        {
            LeftNear,
            LeftFar,
            RightNear,
            RightFar,
            BottomNear,
            BottomFar,
            TopNear,
            TopFar,
            LeftBottom,
            LeftTop,
            RightBottom,
            RightTop
        }

        public enum EFace
        {
            Left,
            Right,
            Bottom,
            Top,
            Near,
            Far
        }

        public GLViewFrustum(Matrix4x4 inViewMatrix, Matrix4x4 inProjectionMatrix)
        {
            ViewMatrix = inViewMatrix;
            InvViewMatrix = ViewMatrix.Inversed;
            ProjectionMatrix = inProjectionMatrix;
            InvProjectionMatrix = ProjectionMatrix.Inversed;

            m_Vertices[EVertex.LeftBottomNear]  = new Vector3(-1.0f, -1.0f, -1.0f);
            m_Vertices[EVertex.LeftBottomFar]   = new Vector3(-1.0f, -1.0f,  1.0f);
            m_Vertices[EVertex.LeftTopNear]     = new Vector3(-1.0f,  1.0f, -1.0f);
            m_Vertices[EVertex.LeftTopFar]      = new Vector3(-1.0f,  1.0f,  1.0f);
            m_Vertices[EVertex.RightBottomNear] = new Vector3( 1.0f, -1.0f, -1.0f);
            m_Vertices[EVertex.RightBottomFar]  = new Vector3( 1.0f, -1.0f,  1.0f);
            m_Vertices[EVertex.RightTopNear]    = new Vector3( 1.0f,  1.0f, -1.0f);
            m_Vertices[EVertex.RightTopFar]     = new Vector3( 1.0f,  1.0f,  1.0f);

            foreach (EVertex vertex in m_Vertices.Keys) {
                Vector4 v = InvProjectionMatrix.Multiply(m_Vertices[vertex], 1.0f);
                v = v / v.w;

                v = InvViewMatrix.Multiply(new Vector3(v), 1.0f);

                m_Vertices[vertex] = new Vector3(v);
            }
            return;
        }

        public bool IsIntersectAABB(AABB3D inAABB)
        {
            // check AABB contains frustum's vertices
            foreach (var vertex in m_Vertices.Values) {
                if (inAABB.Contains(vertex)) {
                    return true;
                }
            }

            // check frustum contains AABB's vertices
            foreach (var vertex in AABBVertices(inAABB).Select(v => WorldToClipCoordinate(v))) {
                if (vertex.x.InRange(-1.0f, 1.0f)
                 && vertex.y.InRange(-1.0f, 1.0f)
                 && vertex.z.InRange(-1.0f, 1.0f)) {
                    return true;
                }
            }

            // check AABB intersects frutsum's edge
            foreach (EEdge edge in Enum.GetValues(typeof(EEdge))) {
                if (!inAABB.IsIntersectWith(GetEdge(edge)).IsEmpty()) {
                    return true;
                }
            }
            return false;
        }

        public bool IsIntersectSphere(Vector3 inWorldCenter, float inRadius)
            => CalcDistanceTo(inWorldCenter) <= inRadius;

        public float CalcDistanceTo(Vector3 inWorldPoint)
            => (CalcClosestPoint(inWorldPoint) - inWorldPoint).Length;

        public Vector3 CalcClosestPoint(Vector3 inWorldPoint)
        {
            var point = WorldToClipCoordinate(inWorldPoint);

            const float r = 1.0f;
            int Place(float inValue)
            {
                if (inValue <= -r) {
                    return 0x00;
                } else if (inValue >= r) {
                    return 0x10;
                } else {
                    return 0x01;
                }
            }
            Vector3 WithEdge(EEdge inLine) => GetEdge(inLine).Project(inWorldPoint);
            Vector3 WithPlane(EFace inPlane) => GetPlane(inPlane).Project(inWorldPoint);
            int place = Place(point.x) | (Place(point.y) << 8) | (Place(point.z) << 16);
            switch (place) {
                case 0x000000:
                    return GetVertex(EVertex.LeftBottomNear);
                case 0x000001: 
                    return WithEdge(EEdge.BottomNear);
                case 0x000010:
                    return GetVertex(EVertex.RightBottomNear);

                case 0x000100:
                    return WithEdge(EEdge.LeftNear);
                case 0x000101:
                    return WithPlane(EFace.Near);
                case 0x000110:
                    return WithEdge(EEdge.RightNear);

                case 0x001000:
                    return GetVertex(EVertex.LeftTopNear);
                case 0x001001:
                    return WithEdge(EEdge.TopNear);
                case 0x001010:
                    return GetVertex(EVertex.RightTopNear);

                case 0x010000:
                    return WithEdge(EEdge.LeftBottom);
                case 0x010001:
                    return WithPlane(EFace.Bottom);
                case 0x010010:
                    return WithEdge(EEdge.RightBottom);

                case 0x010100:
                    return WithPlane(EFace.Left);
                case 0x010101: // in frustum
                    return inWorldPoint;
                case 0x010110:
                    return WithPlane(EFace.Right);

                case 0x011000:
                    return WithEdge(EEdge.LeftTop);
                case 0x011001:
                    return WithPlane(EFace.Top);
                case 0x011010:
                    return WithEdge(EEdge.RightTop);

                case 0x100000:
                    return GetVertex(EVertex.LeftBottomFar);
                case 0x100001:
                    return WithEdge(EEdge.BottomFar);
                case 0x100010:
                    return GetVertex(EVertex.RightBottomFar);

                case 0x100100:
                    return WithEdge(EEdge.LeftFar);
                case 0x100101:
                    return WithPlane(EFace.Far);
                case 0x100110:
                    return WithEdge(EEdge.RightFar);

                case 0x101000:
                    return GetVertex(EVertex.LeftTopFar);
                case 0x101001:
                    return WithEdge(EEdge.TopFar);
                case 0x101010:
                    return GetVertex(EVertex.RightTopFar);
                    
            }
            throw new InvalidProgramException($"{point}, {place:X}");
        }

        public Vector3 GetVertex(EVertex inKey) => m_Vertices[inKey];
        public Line3D GetEdge(EEdge inKey)
        {
            Line3D Get(EVertex v1, EVertex v2) => new Line3D(GetVertex(v1), GetVertex(v2));
            switch (inKey) {
                case EEdge.LeftNear:
                    return Get(EVertex.LeftBottomNear, EVertex.LeftTopNear);
                case EEdge.LeftFar:
                    return Get(EVertex.LeftBottomFar, EVertex.LeftTopFar);
                case EEdge.RightNear:
                    return Get(EVertex.RightBottomNear, EVertex.RightTopNear);
                case EEdge.RightFar:
                    return Get(EVertex.RightBottomFar, EVertex.RightTopFar);
                case EEdge.BottomNear:
                    return Get(EVertex.LeftBottomNear, EVertex.RightBottomNear);
                case EEdge.BottomFar:
                    return Get(EVertex.LeftBottomFar, EVertex.RightBottomFar);
                case EEdge.TopNear:
                    return Get(EVertex.LeftTopNear, EVertex.RightTopNear);
                case EEdge.TopFar:
                    return Get(EVertex.LeftTopFar, EVertex.RightTopFar);
                case EEdge.LeftBottom:
                    return Get(EVertex.LeftBottomNear, EVertex.LeftBottomFar);
                case EEdge.LeftTop:
                    return Get(EVertex.LeftTopNear, EVertex.LeftTopFar);
                case EEdge.RightBottom:
                    return Get(EVertex.RightBottomNear, EVertex.RightBottomFar);
                case EEdge.RightTop:
                    return Get(EVertex.RightTopNear, EVertex.RightTopFar);
            }
            throw new InvalidEnumValueException<EEdge>(inKey);
        }

        public Plane GetPlane(EFace inKey)
        {
            switch (inKey) {
                case EFace.Left:
                    return ToPlane(GetVertex(EVertex.LeftBottomFar), GetVertex(EVertex.LeftBottomNear), GetVertex(EVertex.LeftTopNear), GetVertex(EVertex.LeftTopFar));
                case EFace.Right:
                    return ToPlane(GetVertex(EVertex.RightBottomFar), GetVertex(EVertex.RightTopFar), GetVertex(EVertex.RightTopNear), GetVertex(EVertex.RightBottomNear));
                case EFace.Bottom:
                    return ToPlane(GetVertex(EVertex.LeftBottomFar), GetVertex(EVertex.RightBottomFar), GetVertex(EVertex.RightBottomNear), GetVertex(EVertex.LeftBottomNear));
                case EFace.Top:
                    return ToPlane(GetVertex(EVertex.LeftTopFar), GetVertex(EVertex.LeftTopNear), GetVertex(EVertex.RightTopNear), GetVertex(EVertex.RightTopFar));
                case EFace.Near:
                    return ToPlane(GetVertex(EVertex.LeftTopNear), GetVertex(EVertex.LeftBottomNear), GetVertex(EVertex.RightBottomNear), GetVertex(EVertex.RightTopNear));
                case EFace.Far:
                    return ToPlane(GetVertex(EVertex.LeftTopFar), GetVertex(EVertex.RightTopFar), GetVertex(EVertex.RightBottomFar), GetVertex(EVertex.LeftBottomFar));
            }
            throw new InvalidEnumValueException<EFace>(inKey);
        }

        // must unti-clockwise 4 vertices
        private Plane ToPlane(params Vector3[] inVertices)
            => new Plane(
                    ((IEnumerable<Vector3>)inVertices).Average(),
                    Vector3.Cross((inVertices[1] - inVertices[0]).Normalized, (inVertices[2] - inVertices[0]).Normalized).Normalized
                );

        private Vector3 WorldToClipCoordinate(Vector3 inWorldPoint)
        {
            // world to view coordinate
            Vector4 point = ViewMatrix.Multiply(inWorldPoint, 1.0f);
            // view to clip coordinate
            point = ProjectionMatrix.Multiply(point);
            point /= point.w;
            return new Vector3(point);
        }

        private IEnumerable<Vector3> AABBVertices(AABB3D inAABB)
        {
            var min = inAABB.Min;
            var max = inAABB.Max;

            yield return min;
            yield return new Vector3(min.x, min.y, max.z);
            yield return new Vector3(min.x, max.y, min.z);
            yield return new Vector3(min.x, max.y, max.z);
            yield return new Vector3(max.x, min.y, min.z);
            yield return new Vector3(max.x, min.y, max.z);
            yield return new Vector3(max.x, max.y, min.z);
            yield return max;
        }

        public readonly Matrix4x4 ViewMatrix;
        private readonly Matrix4x4 InvViewMatrix;
        public readonly Matrix4x4 ProjectionMatrix;
        private readonly Matrix4x4 InvProjectionMatrix;

        private EnumArray<EVertex, Vector3> m_Vertices = new EnumArray<EVertex, Vector3>();
    }
}
