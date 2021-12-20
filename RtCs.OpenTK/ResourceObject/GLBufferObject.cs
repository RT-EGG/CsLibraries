using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public class GLBufferObject : GLResourceIdObject
    {
        public void UpdateBuffer<T>(int inSize, T[] inValues, BufferUsageHint inUsageHint) where T : unmanaged
        {
            GLMainThreadTask.CreateNew(_ => {
                GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
                GL.BufferData(BufferTarget.ArrayBuffer, inSize, inValues, inUsageHint);
            });
            return;
        }

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
