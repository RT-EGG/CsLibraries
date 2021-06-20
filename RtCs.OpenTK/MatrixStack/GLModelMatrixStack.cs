using OpenTK.Graphics.OpenGL;

namespace RtCs.OpenGL
{
    public class GLModelMatrixStack : GLMatrixStack
    {
        protected override MatrixMode TargetMatrixMode => MatrixMode.Modelview;
    }
}
