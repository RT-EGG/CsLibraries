namespace RtCs.OpenGL
{
    /// <summary>
    /// The object to render from mesh and material.
    /// </summary>
    public class GLRenderer : GLObject
    {
        /// <summary>
        /// Get or set mesh.
        /// </summary>
        public GLMesh Mesh
        { get; set; } = null;
        /// <summary>
        /// Get or set material.
        /// </summary>
        public GLMaterial Material
        { get; set; } = null;

        internal void Render()
        {
            if ((m_Renderer == null) || (m_Renderer.Mesh != Mesh) || (m_Renderer.Shader != Material.Shader)) {
                m_Renderer = GLMeshRendererDictionary.Instance.Query(Mesh, Material.Shader);
            }

            m_Renderer.Render();
        }

        private GLMeshRenderer m_Renderer = null;

        private class VertexBufferBindingPoint
        {
            public VertexBufferBindingPoint(int inBindingIndex, int inOffsetInAttribute)
            {
                BindingIndex = inBindingIndex;
                OffsetInAttribute = inOffsetInAttribute;
            }

            public readonly int BindingIndex;
            public readonly int OffsetInAttribute;
        }
    }
}
