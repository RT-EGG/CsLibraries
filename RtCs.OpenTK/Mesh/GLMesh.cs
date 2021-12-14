using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.MathUtils.Geometry;
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

    /// <summary>
    /// Object for rendering that is constructed from basic shapes (triangles, quads, line segments, or points).
    /// </summary>
    public class GLMesh : GLObject, ILineIntersectable3D
    {
        /// <summary>
        /// Apply all changes.
        /// </summary>
        /// <remarks>
        /// Even if make changes by any method, the changes will not be applied internally until call this method.
        /// </remarks>
        public void Apply()
            => new GLMainThreadTask(_ => UpdateBuffer()) {
                DoSoonIfCan = false
            }.Enqueue();

        /// <summary>
        /// Position of vertices.
        /// </summary>
        public Vector3[] Vertices
        { get; set; } = default;
        /// <summary>
        /// Normal of vertices.
        /// </summary>
        public Vector3[] Normals
        { get; set; } = default;
        /// <summary>
        /// Texture coordinate of vertices.
        /// </summary>
        public Vector2[] TexCoords
        { get; set; } = default;
        /// <summary>
        /// Color of vertices.
        /// </summary>
        public Vector4[] Colors
        { get; set; } = default;

        /// <summary>
        /// Type of basic shape of the mesh object.
        /// </summary>
        public EGLMeshTopology Topology
        { get; set; } = EGLMeshTopology.Triangles;
        /// <summary>
        /// Array of ID construct each topology.
        /// </summary>
        /// <remarks>
        /// The length of the array is a multiple of the number of vertices that make up a single topology 
        /// (triangle:3*n, quads:4*n, points:1*n, lines:2*n).
        /// </remarks>
        public int[] Indices
        { get; set; } = default;

        /// <summary>
        /// The hint how the data store of vertex buffer object will be accessed.
        /// </summary>
        /// <remarks>
        /// To understand, see OpenGL official [reference](https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/glBufferData.xhtml).
        /// </remarks>
        public BufferUsageHint VertexBufferUsageHint
        { get; set; } = BufferUsageHint.StaticDraw;
        /// <summary>
        /// The hint how the data store of index buffer object will be accessed.
        /// </summary>
        /// <remarks>
        /// To understand, see OpenGL official [reference](https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/glBufferData.xhtml).
        /// </remarks>
        public BufferUsageHint IndexBufferUsageHint
        { get; set; } = BufferUsageHint.StaticDraw;

        /// <summary>
        /// Number of topology.
        /// </summary>
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

        /// <summary>
        /// Bounds used for various culling.
        /// </summary>
        public AABB3D BoundingBox
        { get; set; } = default;

        /// <summary>
        /// Clean up all buffers.
        /// </summary>
        public void Clear()
        {
            Vertices = null;
            Normals = null;
            TexCoords = null;
            Colors = null;
            Indices = null;
            BoundingBox = default;
            return;
        }

        /// <summary>
        /// Update BoundingBox by Vertices.
        /// </summary>
        public void CalculateBoundingBox()
        {
            if (Vertices.IsNullOrEmpty()) {
                BoundingBox = default;
                return;
            }

            BoundingBox = AABB3D.InclusionBoundary(Vertices);
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
            if (!Vertices.IsNullOrEmpty()) {
                AddAttribute(EGLVertexAttributeType.Position, new GLVertexAttribute.Position { DataOffset = offset, Data = Vertices.ToFloatArray() });
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
            GL.BufferData(BufferTarget.ArrayBuffer, buffer.Length * sizeof(float), buffer, VertexBufferUsageHint);

            var indices = Indices;
            if (!indices.IsNullOrEmpty()) {
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, IndexBufferUsageHint);
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

        /// <summary>
        /// Information of intersection with the line.
        /// </summary>
        /// <param name="inLine"></param>
        /// <returns>Intersection information.</returns>
        /// <remarks>
        /// Not support the case that the topology is lines or points.
        /// </remarks>
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

        /// <summary>
        /// Get all triangles.
        /// </summary>
        /// <returns>Triangles construct the mesh.</returns>
        /// <exception cref="InvalidOperationException">The case that the Topology is not Triangle.</exception>
        public IEnumerable<Triangle3D> GetTriangles()
        {
            if (Topology != EGLMeshTopology.Triangles) {
                throw new InvalidOperationException($"Mesh topology is not {EGLMeshTopology.Triangles}, but {Topology}.");
            }

            for (int i = 0; i < Indices.Length; i += 3) {
                yield return new Triangle3D(
                        Vertices[Indices[i + 0]],
                        Vertices[Indices[i + 1]],
                        Vertices[Indices[i + 2]]
                    );
            }
        }

        /// <summary>
        /// Get all Quad divided to 2 triangles.
        /// </summary>
        /// <returns>Quads as devided to 2 triangles construct the mesh.</returns>
        /// <exception cref="InvalidOperationException">The case that the Topology is not Quads.</exception>
        public IEnumerable<Triangle3D> GetDividedQuads()
        {
            if (Topology != EGLMeshTopology.Quads) {
                throw new InvalidOperationException($"Mesh topology is not {EGLMeshTopology.Quads}, but {Topology}.");
            }

            for (int i = 0; i < Indices.Length; i += 4) {
                yield return new Triangle3D(
                        Vertices[Indices[i + 0]],
                        Vertices[Indices[i + 1]],
                        Vertices[Indices[i + 2]]
                    );
                yield return new Triangle3D(
                        Vertices[Indices[i + 0]],
                        Vertices[Indices[i + 2]],
                        Vertices[Indices[i + 3]]
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
