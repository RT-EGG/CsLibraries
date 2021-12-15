namespace RtCs.OpenGL
{
    /// <summary>
    /// Data store for rendering.
    /// </summary>
    public class GLRenderingStatus : GLObject
    {
        /// <summary>
        /// Region of render target.
        /// </summary>
        public GLViewport Viewport
        { get; } = new GLViewport();
        /// <summary>
        /// Matrix stack for model and view transform.
        /// </summary>
        public GLModelviewMatrixStack ModelViewMatrix
        { get; } = new GLModelviewMatrixStack();
        /// <summary>
        /// Matrix stack for projection.
        /// </summary>
        public GLProjectionMatrixStack ProjectionMatrix
        { get; } = new GLProjectionMatrixStack();
    }
}
