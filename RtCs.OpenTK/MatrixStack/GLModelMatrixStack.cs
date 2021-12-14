using OpenTK.Graphics.OpenGL;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The matrix stack used for model matrix.
    /// </summary>
    public class GLModelMatrixStack : GLMatrixStack
    {
        protected override MatrixMode TargetMatrixMode => MatrixMode.Modelview;
    }
}
