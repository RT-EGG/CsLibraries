using OpenTK.Graphics.OpenGL4;

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
    }
}
