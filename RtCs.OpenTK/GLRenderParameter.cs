namespace RtCs.OpenGL
{
    /// <summary>
    /// Data store for rendering.
    /// </summary>
    public class GLRenderParameter : GLObject
    {
        public GLRenderParameter(IGLScene inScene = null)
        {
            Scene = inScene;
            return;
        }

        /// <summary>
        /// Region of render target.
        /// </summary>
        public readonly GLViewport Viewport = new GLViewport();
        /// <summary>
        /// Matrix stack for model and view transform.
        /// </summary>
        public GLModelviewMatrixStack ModelViewMatrix = new GLModelviewMatrixStack();
        /// <summary>
        /// Matrix stack for projection.
        /// </summary>
        public GLProjectionMatrixStack ProjectionMatrix = new GLProjectionMatrixStack();

        public GLBufferObject ModelMatrixBuffer
        { get; private set; } = null;
        public GLBufferObject ViewMatrixBuffer
        { get; private set; } = null;
        public GLBufferObject ModelViewMatrixBuffer 
        { get; private set; } = null;
        public GLBufferObject NormalMatrixBuffer 
        { get; private set; } = null;
        public GLBufferObject ProjectionMatrixBuffer 
        { get; private set; } = null;
        public GLBufferObject ViewProjectionMatrixBuffer 
        { get; private set; } = null;
        public GLBufferObject ModelViewProjectionMatrixBuffer
        { get; private set; } = null;
        //public GLBufferObject

        /// <summary>
        /// Rendering scene.
        /// </summary>
        public readonly IGLScene Scene = null;
    }
}
