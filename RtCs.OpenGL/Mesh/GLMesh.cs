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
        Lines,
        PatchedTriangles,
        PatchedQuads,
    }

    public static class EGLMeshTopologyExtensions
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
                case EGLMeshTopology.PatchedTriangles:
                case EGLMeshTopology.PatchedQuads:
                    return PrimitiveType.Patches;
            }
            throw new InvalidEnumValueException<EGLMeshTopology>(inValue);
        }
    }

    /// <summary>
    /// Object for rendering that is constructed from basic shapes (triangles, quads, line segments, or points).
    /// </summary>
    public class GLMesh : GLObject, ILineIntersectable3D
    {
        public GLMesh()
        {
            VertexPositionAttribute = AddAttribute<Vector3>(GLVertexAttribute.AttributeName_Vertex,
                                                            new GLVertexAttributeDescriptor(GLVertexAttribute.AttributeName_Vertex, 3, sizeof(float)));
        }

        /// <summary>
        /// Apply all changes.
        /// </summary>
        /// <remarks>
        /// Even if make changes by any method, the changes will not be applied internally until call this method.
        /// </remarks>
        public void Apply()
            => GLMainThreadTask.CreateNew(_ => UpdateBuffer());

        /// <summary>
        /// The position attribute of vertex.
        /// </summary>
        public Vector3[] Vertices
        {
            get => VertexPositionAttribute.Buffer;
            set => VertexPositionAttribute.Buffer = value;
        }

        /// <summary>
        /// Add custom attribute for vertex.
        /// </summary>
        /// <typeparam name="T">The type of attribute.</typeparam>
        /// <param name="inDescriptor">The description of attribute,</param>
        public IGLVertexAttribute<T> AddAttribute<T>(string inName, GLVertexAttributeDescriptor inDescriptor) where T : unmanaged
        {
            var newAttribute = new GLVertexAttribute<T>(inName, inDescriptor);
            m_VertexAttributes.Add(newAttribute);

            return newAttribute;
        }

        /// <summary>
        /// Add custom attribute for vertex.
        /// </summary>
        /// <typeparam name="T">The type of attribute.</typeparam>
        /// <param name="inDescriptor">The description of attribute,</param>
        public IGLVertexAttribute<T> AddAttribute<T>(string inName, IEnumerable<GLVertexAttributeDescriptor> inDescriptors) where T : unmanaged
        {
            var newAttribute = new GLVertexAttribute<T>(inName, inDescriptors.ToArray());
            m_VertexAttributes.Add(newAttribute);

            return newAttribute;
        }

        /// <summary>
        /// Add custom attribute for vertex.
        /// </summary>
        /// <typeparam name="T">The type of attribute.</typeparam>
        /// <param name="inDescriptor">The description of attribute,</param>
        public IGLVertexAttribute<T> AddAttribute<T>(string inName, params GLVertexAttributeDescriptor[] inDescriptors) where T : unmanaged
        {
            var newAttribute = new GLVertexAttribute<T>(inName, inDescriptors);
            m_VertexAttributes.Add(newAttribute);

            return newAttribute;
        }

        /// <summary>
        /// Get added custom attribute.
        /// </summary>
        /// <typeparam name="T">The type of attribute.</typeparam>
        /// <param name="inName">The name to identify.</param>
        /// <returns></returns>
        public IGLVertexAttribute<T> GetAttribute<T>(string inName) where T : unmanaged
        {
            if (m_VertexAttributes.TryGetFirst(out var att, a => a.Name == inName)) {
                return att as GLVertexAttribute<T>;
            }
            return null;
        }

        internal GLVertexAttributeList VertexAttributes => m_VertexAttributes;

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
            int bufferSize = 0;
            foreach (var attribute in m_VertexAttributes) {
                bufferSize += attribute.BufferSize;
            }

            m_VertexBufferDescriptors.Clear();
            byte[] buffer = new byte[bufferSize];
            int offset = 0;
            foreach (var attribute in m_VertexAttributes) {
                attribute.Offset = offset;
                int descOffset = offset;
                int stride = attribute.Stride;

                foreach (var desc in attribute.Descriptors) {
                    m_VertexBufferDescriptors.Add(new GLVertexBufferDescriptor(descOffset, desc));
                    descOffset += desc.Stride;
                }

                offset += attribute.CopyToBuffer(buffer, offset);
            }

            VertexBuffer.AllocateBuffer(buffer.Length, buffer, VertexBufferUsageHint);

            var indices = Indices;
            if (!indices.IsNullOrEmpty()) {
                IndexBuffer.AllocateBuffer(indices.Length * sizeof(int), indices, IndexBufferUsageHint);
            }

            AfterBufferUpdate?.Invoke(this, EventArgs.Empty);
            return;
        }

        public bool TryGetVertexBindingPoint(string inElementName, out int outPoint)
        {
            if (m_VertexBufferDescriptors.TryGetFirst(out var desc, d => d.AttributeDescriptor.Name == inElementName)) {
                outPoint = desc.Offset;
                return true;
            }
            outPoint = -1;
            return false;
        }
        //=> m_VertexBufferDescriptors.TryGetFirst()
        //=> m_VertexBindingPoints.TryGetValue(inElementName, out outPoint);

        internal IReadOnlyList<GLVertexBufferDescriptor> VertexBufferDescriptors => m_VertexBufferDescriptors;

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                VertexBuffer?.Dispose();
                IndexBuffer?.Dispose();
            }
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

        public GLBufferObject VertexBuffer
        { get; } = new GLBufferObject();
        public GLBufferObject IndexBuffer
        { get; } = new GLBufferObject();

        public event EventHandler AfterBufferUpdate;

        private List<GLVertexBufferDescriptor> m_VertexBufferDescriptors = new List<GLVertexBufferDescriptor>();

        private IGLVertexAttribute<Vector3> VertexPositionAttribute
        { get; } = null;
        private GLVertexAttributeList m_VertexAttributes = new GLVertexAttributeList();
    }
}
