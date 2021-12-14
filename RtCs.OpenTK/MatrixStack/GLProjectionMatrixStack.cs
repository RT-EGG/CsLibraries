using OpenTK.Graphics.OpenGL;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The matrix stack used for projection matrix.
    /// </summary>
    public class GLProjectionMatrixStack : GLMatrixStack
    {
        protected override MatrixMode TargetMatrixMode => MatrixMode.Projection;
    }
}
