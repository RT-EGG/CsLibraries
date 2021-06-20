using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLShaderProgram : GLResourceIdObject
    {
        public void AttachShader(GLShader inShader)
            => GL.AttachShader(ID, inShader.ID);
        public void DetachShader(GLShader inShader)
            => GL.DetachShader(ID, inShader.ID);

        public bool Link()
        {
            m_LinkError.Clear();

            GL.LinkProgram(ID);
            GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out m_LinkState);

            if (!Linked) {
                m_LinkError.AddRange(GL.GetProgramInfoLog(ID).Split('\n'));
            }
            return Linked;
        }

        protected override void InternalCreateResource()
        {
            base.InternalCreateResource();
            if (ID == 0) {
                ID = GL.CreateProgram();
            }
            return;
        }

        protected override void InternalDestroyResource()
        {
            base.InternalDestroyResource();
            if (ID != 0) {
                GL.DeleteProgram(ID);
            }
            return;
        }

        public bool Linked => m_LinkState != 0;
        public IReadOnlyList<string> LinkError => m_LinkError;

        private int m_LinkState = 0;
        private List<string> m_LinkError = new List<string>();
    }
}
