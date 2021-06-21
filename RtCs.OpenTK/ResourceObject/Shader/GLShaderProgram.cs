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

            if (Linked) {
                CollectUniformProperties();
            } else {
                m_UniformProperties.Clear();
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

        private void CollectUniformProperties()
        {
            m_UniformProperties.Clear();
            int size, length;

            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out int count);
            for (int i = 0; i < count; ++i) {
                GL.GetActiveUniform(ID, i, 255, out length, out size, out ActiveUniformType type, out string name);

                GLShaderUniformProperty property = CreatePropertyFor(type); ;
                if (property != null) {
                    property.Name = name;
                    m_UniformProperties.Add(property);
                }
            }
            return;
        }

        private static GLShaderUniformProperty CreatePropertyFor(ActiveUniformType inType)
        {
            switch (inType) {
                case ActiveUniformType.Int:
                    return new GLShaderUniformProperty.Int();
                case ActiveUniformType.FloatVec4:
                    return new GLShaderUniformProperty.Vec4();
                case ActiveUniformType.FloatMat4:
                    return new GLShaderUniformProperty.Mat4();
            }
            // not support now...
            return null;
        }

        public bool Linked => m_LinkState != 0;
        public IReadOnlyList<string> LinkError => m_LinkError;

        private int m_LinkState = 0;
        private List<string> m_LinkError = new List<string>();

        private List<GLShaderUniformProperty> m_UniformProperties = new List<GLShaderUniformProperty>();
    }
}
