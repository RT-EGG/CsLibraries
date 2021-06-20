using OpenTK.Graphics.OpenGL;

namespace RtCs.OpenGL
{
    public class GLBufferObject : GLResourceIdObject
    {
        protected override void InternalCreateResource()
        {
            base.InternalCreateResource();
            if (ID == 0) {
                ID = GL.GenBuffer();
            }
            return;
        }

        protected override void InternalDestroyResource()
        {
            base.InternalDestroyResource();
            if (ID != 0) {
                GL.DeleteBuffer(ID);
                ID = 0;
            }
            return;
        }
    }
}
