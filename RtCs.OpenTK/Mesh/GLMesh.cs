using OpenTK.Graphics.OpenGL;
using RtCs.MathUtils;
using System;
using System.Collections.Generic;
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

    public class GLMesh
    {        
        public Vector3[] Positions
        {
            get => m_Positions;
            set {
                m_Positions = value;
                m_BufferUpdateQueue.Enqueue(() => {
                    if (m_Positions.IsNullOrEmpty()) {
                        Buffers.Positions?.Dispose();
                        Buffers.Positions = null;
                    } else {
                        if (Buffers.Positions == null) {
                            Buffers.Positions = new GLBufferObject();
                        }
                        Buffers.Positions.OnAfterCreateResource += _ => {
                            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffers.Positions);
                            GLBufferData(BufferTarget.ArrayBuffer, m_Positions, BufferUsageHint.StaticDraw);
                        };
                    }
                });
            }
        }
        public Vector3[] Normals
        {
            get => m_Normal;
            set {
                m_Normal = value;
                m_BufferUpdateQueue.Enqueue(() => {
                    if (m_Normal.IsNullOrEmpty()) {
                        Buffers.Normals?.Dispose();
                        Buffers.Normals = null;
                    } else {
                        if (Buffers.Normals == null) {
                            Buffers.Normals = new GLBufferObject();
                        }
                        Buffers.Normals.OnAfterCreateResource += _ => {
                            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffers.Normals);
                            GLBufferData(BufferTarget.ArrayBuffer, m_Normal, BufferUsageHint.StaticDraw);
                        };
                    }                    
                });
            }
        }
        public Vector2[] TexCoords
        {
            get => m_TexCoords;
            set {
                m_TexCoords = value;
                m_BufferUpdateQueue.Enqueue(() => {
                    if (m_TexCoords.IsNullOrEmpty()) {
                        Buffers.TexCoords?.Dispose();
                        Buffers.TexCoords = null;
                    } else {
                        if (Buffers.TexCoords == null) {
                            Buffers.TexCoords = new GLBufferObject();
                        }
                        Buffers.TexCoords.OnAfterCreateResource += _ => {
                            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffers.TexCoords);
                            GLBufferData(BufferTarget.ArrayBuffer, m_TexCoords, BufferUsageHint.StaticDraw);
                        };
                    }
                });
            }
        }

        public EGLMeshTopology Topology
        { get; set; } = EGLMeshTopology.Triangles;
        public int[] Indices
        {
            get => m_Indices;
            set {
                m_Indices = value;
                m_BufferUpdateQueue.Enqueue(() => {
                    if (m_Indices.IsNullOrEmpty()) {
                        Buffers.Indices.Dispose();
                        Buffers.Indices = null;
                    } else {
                        if (Buffers.Indices == null) {
                            Buffers.Indices = new GLBufferObject();
                        }
                        Buffers.Indices.OnAfterCreateResource += _ => {
                            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Buffers.Indices);
                            GL.BufferData(BufferTarget.ElementArrayBuffer, m_Indices.Length * sizeof(int), m_Indices, BufferUsageHint.StaticDraw);
                        };
                    }
                });
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
            while (!m_BufferUpdateQueue.IsEmpty()) {
                m_BufferUpdateQueue.Dequeue()();
            }
            return;
        }

        private void GLBufferData(BufferTarget inTarget, Vector3[] inBuffer, BufferUsageHint inHint)
            => GL.BufferData(inTarget, inBuffer.Length * 3 * sizeof(float), inBuffer.Select(v => (IVector)v).Flatten().Select(v => (float)v).ToArray(), inHint);
        private void GLBufferData(BufferTarget inTarget, Vector2[] inBuffer, BufferUsageHint inHint)
            => GL.BufferData(inTarget, inBuffer.Length * 2 * sizeof(float), inBuffer.Select(v => (IVector)v).Flatten().Select(v => (float)v).ToArray(), inHint);

        private Vector3[] m_Positions = null;
        private Vector3[] m_Normal = null;
        private Vector2[] m_TexCoords = null;
        private int[] m_Indices = null;

        private Queue<Action> m_BufferUpdateQueue = new Queue<Action>();
        internal MeshBuffers Buffers = new MeshBuffers();
        internal class MeshBuffers
        {
            public GLBufferObject Positions
            { get; set; } = null;
            public GLBufferObject Normals
            { get; set; } = null;
            public GLBufferObject TexCoords
            { get; set; } = null;
            public GLBufferObject Indices
            { get; set; } = null;
        }
    }
}
