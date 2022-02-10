using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public class GLVertexArrayObject : GLResourceObject
    {
        public GLVertexArrayObject()
            : base()
        { }

        public void Bind()
        {
            if (ID < 0) {
                //GL.CreateVertexArrays(1, out int array);
                int array = GL.GenVertexArray();
                ID = array;
            }
            GL.BindVertexArray(ID);
        }

        protected override void DestroyResourceCore()
        {
            base.DestroyResourceCore();

            if (ID != -1) {
                GL.DeleteVertexArray(ID);
                ID = -1;
            }
        }

        public int ID
        { get; private set; } = -1;

        protected override bool IsResourceCreated => ID != -1;
    }
}
