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
        public GLMesh()
        {
            m_Positions.Skip(1).Subscribe(_ => OnVertexBufferChanged());
            m_Normals.Skip(1).Subscribe(_ => OnVertexBufferChanged());
            m_TexCoords.Skip(1).Subscribe(_ => OnVertexBufferChanged());
            m_Colors.Skip(1).Subscribe(_ => OnVertexBufferChanged());
            m_Indices.Skip(1).Subscribe(_ => OnIndexBufferChanged());
            return;
        }

        public Vector3[] Positions
        { get => m_Positions.Value; set => m_Positions.Value = value; }
        public Vector3[] Normals
        { get => m_Normals.Value; set => m_Normals.Value = value; }
        public Vector2[] TexCoords
        { get => m_TexCoords.Value; set => m_TexCoords.Value = value; }
        public Vector4[] Colors
        { get => m_Colors.Value; set => m_Colors.Value = value; }

        public EGLMeshTopology Topology
        { get; set; } = EGLMeshTopology.Triangles;
        public int[] Indices
        { get => m_Indices.Value; set => m_Indices.Value = value; }

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

        private void UpdateVertexBuffer()
        {
            int offset = 0;
            void AddAttribute(EGLVertexAttributeType inType, GLVertexAttribute inAtt)
            {
                VertexAttributes[inType] = inAtt;
                offset += inAtt.Data.Length;
            }

            VertexAttributes.Clear();
            if (!m_Positions.Value.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.Position, new GLVertexAttribute.Position { DataOffset = offset, Data = m_Positions.Value.ToFloatArray() });
            }
            if (!m_Normals.Value.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.Normal, new GLVertexAttribute.Normal { DataOffset = offset, Data = m_Normals.Value.ToFloatArray() });
            }
            if (!m_TexCoords.Value.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.TexCoord, new GLVertexAttribute.Normal { DataOffset = offset, Data = m_TexCoords.Value.ToFloatArray() });
            }
            if (!m_Colors.Value.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.Color, new GLVertexAttribute.Color { DataOffset = offset, Data = m_Colors.Value.ToFloatArray() });
            }

            float[] buffer = new float[offset];
            foreach (var attrib in VertexAttributes.Cast<GLVertexAttribute>()) {
                Array.Copy(attrib.Data, 0, buffer, attrib.DataOffset, attrib.Data.Length);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, buffer.Length * sizeof(float), buffer, BufferUsageHint);

            m_VertexBufferChanged = false;
            return;
        }

        private void UpdateIndexBuffer()
        {
            var indices = m_Indices.Value;
            if (!indices.IsNullOrEmpty()) {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
            }
            m_IndexBufferChanged = false;
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
            var positions = m_Positions.Value;
            var indices = m_Indices.Value;
            for (int i = 0; i < indices.Length; i += 3) {
                yield return new Triangle3D(
                        positions[indices[i + 0]],
                        positions[indices[i + 1]],
                        positions[indices[i + 2]]
                    );
            }
        }

        public IEnumerable<Triangle3D> GetDividedQuads()
        {
            if (Topology != EGLMeshTopology.Quads) {
                throw new InvalidOperationException($"Mesh topology is not {EGLMeshTopology.Quads}, but {Topology}.");
            }
            var positions = m_Positions.Value;
            var indices = m_Indices.Value;
            for (int i = 0; i < indices.Length; i += 4) {
                yield return new Triangle3D(
                        positions[indices[i + 0]],
                        positions[indices[i + 1]],
                        positions[indices[i + 2]]
                    );
                yield return new Triangle3D(
                        positions[indices[i + 0]],
                        positions[indices[i + 2]],
                        positions[indices[i + 3]]
                    );
            }
        }

        private ModificationRecordValue<Vector3[]> m_Positions = new ModificationRecordValue<Vector3[]>();
        private ModificationRecordValue<Vector3[]> m_Normals = new ModificationRecordValue<Vector3[]>();
        private ModificationRecordValue<Vector2[]> m_TexCoords = new ModificationRecordValue<Vector2[]>();
        private ModificationRecordValue<Vector4[]> m_Colors = new ModificationRecordValue<Vector4[]>();
        private ModificationRecordValue<int[]> m_Indices = new ModificationRecordValue<int[]>();

        internal GLBufferObject VertexBuffer
        { get; } = new GLBufferObject();
        private bool m_VertexBufferChanged = false;

        internal GLBufferObject IndexBuffer
        { get; } = new GLBufferObject();
        private bool m_IndexBufferChanged = false;

        private void OnVertexBufferChanged()
        {
            if (!m_VertexBufferChanged) {
                new GLMainThreadTask(_ => UpdateVertexBuffer()) {
                    DoSoonIfCan = false
                }.Enqueue();
            }
            m_VertexBufferChanged = true;
            return;
        }

        private void OnIndexBufferChanged()
        {
            if (!m_IndexBufferChanged) {
                new GLMainThreadTask(_ => UpdateIndexBuffer()) {
                    DoSoonIfCan = false
                }.Enqueue();
            }
            m_IndexBufferChanged = true;
            return;
        }

        public GLVertexAttributeList VertexAttributes
        { get; } = new GLVertexAttributeList();
    }
}
