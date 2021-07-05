using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if (Linked) {
                m_UniformPropertySockets = CollectUniformPropertySockets(ID);
            } else {
                m_UniformPropertySockets.Clear();
                m_LinkError.AddRange(GL.GetProgramInfoLog(ID).Split('\n'));
            }

            OnAfterLinked?.Invoke(this);
            return Linked;
        }

        public IReadOnlyList<GLShaderUniformPropertySocket> UniformPropertySockets
            => m_UniformPropertySockets;
        public GLShaderUniformPropertySocket GetPropertySocket(string inName)
            => UniformPropertySockets.FirstOrDefault(s => s.Name == inName);

        public virtual IEnumerable<GLShaderUniformProperty> CreateDefaultProperties()
            => new GLShaderUniformProperty[0];

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

        protected static List<GLShaderUniformPropertySocket> CollectUniformPropertySockets(int inProgramID)
        {
            GL.GetProgram(inProgramID, GetProgramParameterName.ActiveUniforms, out int count);
            var result = new List<GLShaderUniformPropertySocket>(count);
            
            int size, length;            
            for (int i = 0; i < count; ++i) {
                GL.GetActiveUniform(inProgramID, i, 255, out length, out size, out ActiveUniformType type, out string name);
                int location = GL.GetUniformLocation(inProgramID, name);

                result.Add(new GLShaderUniformPropertySocket(name, location, type));
            }
            return result;
        }

        public bool Linked => m_LinkState != 0;
        public IReadOnlyList<string> LinkError => m_LinkError;

        public event Action<GLShaderProgram> OnAfterLinked;

        private int m_LinkState = 0;
        private List<string> m_LinkError = new List<string>();

        private List<GLShaderUniformPropertySocket> m_UniformPropertySockets = new List<GLShaderUniformPropertySocket>();
    }
}
