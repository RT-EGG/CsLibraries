using OpenTK.Graphics.OpenGL;
using RtCs.MathUtils;
using System;
using System.Linq;

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

    public class GLMesh : GLObject
    {        
        public Vector3[] Positions
        {
            get => m_Positions;
            set {
                m_Positions = value;
                m_VertexBufferChanged = true;
            }
        }
        public Vector3[] Normals
        {
            get => m_Normals;
            set {
                m_Normals = value;
                m_VertexBufferChanged = true;
            }
        }
        public Vector2[] TexCoords
        {
            get => m_TexCoords;
            set {
                m_TexCoords = value;
                m_VertexBufferChanged = true;
            }
        }
        public Vector4[] Colors
        {
            get => m_Colors;
            set {
                m_Colors = value;
                m_VertexBufferChanged = true;
            }
        }

        public EGLMeshTopology Topology
        { get; set; } = EGLMeshTopology.Triangles;
        public int[] Indices
        {
            get => m_Indices;
            set {
                m_Indices = value;
                m_IndexBufferChanged = true;
            }
        }

        public int TopologyCount
        {
            get {
                if (m_Indices.IsNullOrEmpty()) {
                    return 0;
                }

                switch (Topology) {
                    case EGLMeshTopology.Triangles:
                        return m_Indices.Length / 3;
                    case EGLMeshTopology.Quads:
                        return m_Indices.Length / 4;
                    case EGLMeshTopology.Lines:
                        return m_Indices.Length / 2;
                    case EGLMeshTopology.Points:
                        return m_Indices.Length;
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

        internal void UpdateBuffers()
        {
            if (m_VertexBufferChanged) {
                int offset = 0;
                void AddAttribute(EGLVertexAttributeType inType, GLVertexAttribute inAtt)
                {
                    VertexAttributes[inType] = inAtt;
                    offset += inAtt.Data.Length;
                }

                VertexAttributes.Clear();
                if (!m_Positions.IsNullOrEmpty()) {
                    AddAttribute(EGLVertexAttributeType.Position, new GLVertexAttribute.Position { DataOffset = offset, Data = m_Positions.ToFloatArray() });
                }
                if (!m_Normals.IsNullOrEmpty()) {
                    AddAttribute(EGLVertexAttributeType.Normal, new GLVertexAttribute.Normal { DataOffset = offset, Data = m_Normals.ToFloatArray() });
                }
                if (!m_TexCoords.IsNullOrEmpty()) {
                    AddAttribute(EGLVertexAttributeType.TexCoord, new GLVertexAttribute.Normal { DataOffset = offset, Data = m_TexCoords.ToFloatArray() });
                }
                if (!m_Colors.IsNullOrEmpty()) {
                    AddAttribute(EGLVertexAttributeType.Color, new GLVertexAttribute.Color { DataOffset = offset, Data = m_Colors.ToFloatArray() });
                }

                float[] buffer = new float[offset];
                foreach (var attrib in VertexAttributes.Cast<GLVertexAttribute>()) {
                    Array.Copy(attrib.Data, 0, buffer, attrib.DataOffset, attrib.Data.Length);
                }

                GL.BindBuffer(BufferTarget.ArrayBuffer, m_VertexBuffer);
                GL.BufferData(BufferTarget.ArrayBuffer, buffer.Length * sizeof(float), buffer, BufferUsageHint.StaticDraw);

                m_VertexBufferChanged = false;
            }

            if (m_IndexBufferChanged) {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, m_IndexBuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, m_Indices.Length * sizeof(int), m_Indices, BufferUsageHint.StaticDraw);
                m_IndexBufferChanged = false;
            }
            return;
        }

        protected override void Dispose(bool inDisposing)
        {
            base.Dispose(inDisposing);

            if (!Disposed) {
                m_VertexBuffer?.Dispose();
                m_IndexBuffer?.Dispose();
            }
            return;
        }

        private Vector3[] m_Positions = null;
        private Vector3[] m_Normals = null;
        private Vector2[] m_TexCoords = null;
        private Vector4[] m_Colors = null;
        private int[] m_Indices = null;

        private bool m_VertexBufferChanged = true;
        private GLBufferObject m_VertexBuffer = new GLBufferObject();
        internal GLBufferObject VertexBuffer
        {
            get {
                if (m_VertexBufferChanged) {
                    UpdateBuffers();
                }
                return m_VertexBuffer;
            }
        }

        private bool m_IndexBufferChanged = true;
        private GLBufferObject m_IndexBuffer = new GLBufferObject();
        internal GLBufferObject IndexBuffer
        {
            get {
                if (m_IndexBufferChanged) {
                    UpdateBuffers();
                }
                return m_IndexBuffer;
            }
        }

        public GLVertexAttributeList VertexAttributes
        { get; } = new GLVertexAttributeList();
    }
}
