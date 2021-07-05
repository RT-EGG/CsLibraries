using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public class GLMaterial
    {
        public GLMaterial()
        {
            Shader = GLRenderShaderProgram.Preset.Color;
            return;
        }

        public virtual void CommitProperties(GLRenderingStatus inRenderingStatus)
        {
            if (Shader == null) {
                return;
            }

            foreach (var property in m_Properties.Values) {
                property.CommitProperty(Shader);
            }
            return;
        }

        public void AddProperty(GLShaderUniformProperty inProperty)
            => m_Properties.Add(inProperty.Name, inProperty);
        public void RemoveProperty(string inName)
            => m_Properties.Remove(inName);
        public void ClearProperties()
            => m_Properties.Clear();

        public GLShaderUniformProperty<T> GetProperty<T>(string inName)
            => m_Properties[inName] as GLShaderUniformProperty<T>;

        public GLRenderShaderProgram Shader
        {
            get => m_Shader;
            set {
                if (m_Shader != null) {
                    m_Shader.OnAfterLinked -= OnShaderLinked;
                }

                m_Properties.Clear();
                m_Shader = value;
                if (m_Shader != null) {
                    m_Shader.OnAfterLinked += OnShaderLinked;
                }
                ResetupProperties();
                return;
            }
        }

        private void OnShaderLinked(GLShaderProgram inProgram)
            => ResetupProperties();

        private void ResetupProperties()
        {
            Dictionary<string, GLShaderUniformProperty> newList = new Dictionary<string, GLShaderUniformProperty>();
            if (m_Shader != null) {
                foreach (var socket in m_Shader.UniformPropertySockets) {
                    if (newList.TryGetValue(socket.Name, out var property)) {
                        newList.Add(socket.Name, property);
                    }
                }

                foreach (var @default in m_Shader.CreateDefaultProperties()) {
                    if (@default?.Socket == null) {
                        continue;
                    }
                    if (!newList.ContainsKey(@default.Name)) {
                        newList.Add(@default.Name, @default);
                    }
                }
            }

            m_Properties = newList;
            return;
        }

        private Dictionary<string, GLShaderUniformProperty> m_Properties = new Dictionary<string, GLShaderUniformProperty>();
        private GLRenderShaderProgram m_Shader = null;
    }
}
