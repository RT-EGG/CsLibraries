using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace RtCs.OpenGL
{
    public enum EGLMeshTopology
    {
        Triangles,
        Quads,
        Points,
        Lines
    }

    static class EGLMeshTopologyExtensions
    {
        public static PrimitiveType ToPrimitiveType(this EGLMeshTopology inValue)
        {
            switch (inValue) {
                case EGLMeshTopology.Triangles:
                    return PrimitiveType.Triangles;
                case EGLMeshTopology.Quads:
                    return PrimitiveType.Quads;
                case EGLMeshTopology.Points:
                    return PrimitiveType.Points;
                case EGLMeshTopology.Lines:
                    return PrimitiveType.Lines;
            }
            throw new InvalidEnumValueException<EGLMeshTopology>(inValue);
        }
    }

    public class GLMesh : GLObject, ILineIntersectable3D
    {
        public void Apply()
            => new GLMainThreadTask(_ => UpdateBuffer()) {
                DoSoonIfCan = false
            }.Enqueue();

        public Vector3[] Positions
        { get; set; } = default;
        public Vector3[] Normals
        { get; set; } = default;
        public Vector2[] TexCoords
        { get; set; } = default;
        public Vector4[] Colors
        { get; set; } = default;

        public EGLMeshTopology Topology
        { get; set; } = EGLMeshTopology.Triangles;
        public int[] Indices
        { get; set; } = default;

        public BufferUsageHint BufferUsageHint
        { get; set; } = BufferUsageHint.StaticDraw;

        public int TopologyCount
        {
            get {
                if (Indices.IsNullOrEmpty()) {
                    return 0;
                }

                switch (Topology) {
                    case EGLMeshTopology.Triangles:
                        return Indices.Length / 3;
                    case EGLMeshTopology.Quads:
                        return Indices.Length / 4;
                    case EGLMeshTopology.Lines:
                        return Indices.Length / 2;
                    case EGLMeshTopology.Points:
                        return Indices.Length;
                }
                throw new InvalidEnumValueException<EGLMeshTopology>(Topology);
            }
        }

        public AABB3D BoundingBox
        { get; set; } = default;

        public void Clear()
        {
            Positions = null;
            Normals = null;
            TexCoords = null;
            Colors = null;
            Indices = null;
            BoundingBox = default;
            return;
        }

        public void CalculateBoundingBox()
        {
            if (Positions.IsNullOrEmpty()) {
                BoundingBox = default;
                return;
            }

            BoundingBox = AABB3D.InclusionBoundary(Positions);
            return;
        }

        private void UpdateBuffer()
        {
            int offset = 0;
            void AddAttribute(EGLVertexAttributeType inType, GLVertexAttribute inAtt)
            {
                VertexAttributes[inType] = inAtt;
                offset += inAtt.Data.Length;
            }

            VertexAttributes.Clear();
            if (!Positions.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.Position, new GLVertexAttribute.Position { DataOffset = offset, Data = Positions.ToFloatArray() });
            }
            if (!Normals.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.Normal, new GLVertexAttribute.Normal { DataOffset = offset, Data = Normals.ToFloatArray() });
            }
            if (!TexCoords.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.TexCoord, new GLVertexAttribute.TexCoord { DataOffset = offset, Data = TexCoords.ToFloatArray() });
            }
            if (!Colors.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.Color, new GLVertexAttribute.Color { DataOffset = offset, Data = Colors.ToFloatArray() });
            }

            float[] buffer = new float[offset];
            foreach (var attrib in VertexAttributes.Cast<GLVertexAttribute>()) {
                Array.Copy(attrib.Data, 0, buffer, attrib.DataOffset, attrib.Data.Length);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, buffer.Length * sizeof(float), buffer, BufferUsageHint);

            var indices = Indices;
            if (!indices.IsNullOrEmpty()) {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
            }
            return;
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            VertexBuffer?.Dispose();
            IndexBuffer?.Dispose();
            return;
        }

        public virtual IEnumerable<LineIntersectionInfo3D> IsIntersectWith(Line3D inLine)
        {
            switch (Topology) {
                case EGLMeshTopology.Triangles:
                    foreach (var triangle in GetTriangles()) {
                        if (triangle.IsIntersectWith(inLine, out var info)) {
                            yield return info;
                        }
                    }
                    break;
                case EGLMeshTopology.Quads:
                    foreach (var triangle in GetDividedQuads()) {
                        if (triangle.IsIntersectWith(inLine, out var info)) {
                            yield return info;
                        }
                    }
                    break;
            }
            yield break;
        }

        public IEnumerable<Triangle3D> GetTriangles()
        {
            if (Topology != EGLMeshTopology.Triangles) {
                throw new InvalidOperationException($"Mesh topology is not {EGLMeshTopology.Triangles}, but {Topology}.");
            }

            for (int i = 0; i < Indices.Length; i += 3) {
                yield return new Triangle3D(
                        Positions[Indices[i + 0]],
                        Positions[Indices[i + 1]],
                        Positions[Indices[i + 2]]
                    );
            }
        }

        public IEnumerable<Triangle3D> GetDividedQuads()
        {
            if (Topology != EGLMeshTopology.Quads) {
                throw new InvalidOperationException($"Mesh topology is not {EGLMeshTopology.Quads}, but {Topology}.");
            }

            for (int i = 0; i < Indices.Length; i += 4) {
                yield return new Triangle3D(
                        Positions[Indices[i + 0]],
                        Positions[Indices[i + 1]],
                        Positions[Indices[i + 2]]
                    );
                yield return new Triangle3D(
                        Positions[Indices[i + 0]],
                        Positions[Indices[i + 2]],
                        Positions[Indices[i + 3]]
                    );
            }
        }

        internal GLBufferObject VertexBuffer
        { get; } = new GLBufferObject();
        internal GLBufferObject IndexBuffer
        { get; } = new GLBufferObject();

        public GLVertexAttributeList VertexAttributes
        { get; } = new GLVertexAttributeList();
    }
}
