using OpenTK.Graphics.OpenGL;

namespace RtCs.OpenGL
{
    public class GLProjectionMatrixStack : GLMatrixStack
    {
        protected override MatrixMode TargetMatrixMode => MatrixMode.Projection;
    }
}
