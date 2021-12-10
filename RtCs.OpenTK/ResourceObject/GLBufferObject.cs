using OpenTK.Graphics.OpenGL;

namespace RtCs.OpenGL
{
    public class GLBufferObject : GLResourceIdObject
    {
        protected override void CreateResourceCore()
        {
            base.CreateResourceCore();
            if (ID == 0) {
                ID = GL.GenBuffer();
            }
            return;
        }

        protected override void DestroyResourceCore()
        {
            base.DestroyResourceCore();
            if (ID != 0) {
                GL.DeleteBuffer(ID);
                ID = 0;
            }
            return;
        }
    }
}
