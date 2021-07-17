namespace RtCs.OpenGL
{
    public class GLRenderingStatus : GLObject
    {
        public GLViewport Viewport
        { get; } = new GLViewport();
        public GLModelviewMatrixStack ModelViewMatrix
        { get; } = new GLModelviewMatrixStack();
        public GLProjectionMatrixStack ProjectionMatrix
        { get; } = new GLProjectionMatrixStack();
    }
}
