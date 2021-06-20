using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLMaterial
    {
        public void CommitProperties(int inProgramID)
        {
            foreach (var (_, property) in m_Properties) {
                property.CommitProperty(inProgramID);
            }
            return;
        }

        public void AddProperty(GLShaderUniformProperty inProperty)
            => m_Properties.Add(inProperty.Name, inProperty);
        public void RemoveProperty(string inName)
            => m_Properties.Remove(inName);

        public GLShaderUniformProperty<T> GetProperty<T>(string inName)
            => m_Properties[inName] as GLShaderUniformProperty<T>;

        public GLShaderProgram Shader
        { get; set; } = null;

        private Dictionary<string, GLShaderUniformProperty> m_Properties = new Dictionary<string, GLShaderUniformProperty>();
    }
}
