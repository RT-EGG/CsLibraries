using OpenTK.Graphics.OpenGL4;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    /// <summary>
    /// Value to decide the rendering order and parameters.
    /// </summary>
    public enum EGLRenderLevel
    {
        /// <summary>
        /// No transparency part, for general object.
        /// </summary>
        Opaque = 0,
        /// <summary>
        /// Has transparency part (non-1 alpha).
        /// </summary>
        Transparent,
        /// <summary>
        /// Overwrite all.
        /// </summary>
        Overlay
    }

    /// <summary>
    /// Object that decide render color using shader and properties for the shader.
    /// </summary>
    public class GLMaterial : GLObject
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

            GLShaderUniformProperty.CommitStatus commitState = new GLShaderUniformProperty.CommitStatus {
                CurrentAvailableTextureUnit = TextureUnit.Texture0
            };
            foreach (var property in m_Properties.Values) {
                property.CommitProperty(Shader, commitState);
            }
            return;
        }

        // TODO remove list access public methods.
        public void AddProperty(GLShaderUniformProperty inProperty)
            => m_Properties.Add(inProperty.Name, inProperty);
        public void RemoveProperty(string inName)
            => m_Properties.Remove(inName);
        public void ClearProperties()
            => m_Properties.Clear();

        // TODO add TryGet pattern.
        /// <summary>
        /// Get property by name.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="inName">Name of property.</param>
        /// <returns>The property if found by name and property type is T, otherwise null.</returns>
        public GLShaderUniformProperty<T> GetProperty<T>(string inName)
        {
            if (m_Properties.TryGetValue(inName, out var property)) {
                return property as GLShaderUniformProperty<T>;
            }
            return null;
        }

        /// <summary>
        /// Set property value.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="inName">Name of property.</param>
        /// <param name="inValue">Value of property.</param>
        /// <remarks>
        /// If not found property inName, or the property is not T, do nothing.
        /// </remarks>
        public void SetPropertyValue<T>(string inName, T inValue)
        {
            var property = GetProperty<T>(inName);
            if (property != null) {
                property.Value = inValue;
            }
            return;
        }

        /// <summary>
        /// Set or get shader.
        /// </summary>
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

        /// <summary>
        /// Set or get parameter for blending.
        /// </summary>
        public IGLBlendParameters BlendParameters
        { get; set; } = GLBlendParameters.Default;

        /// <summary>
        /// Value to decide the rendering order and parameters.
        /// </summary>
        public EGLRenderLevel RenderLevel
        { get; set; } = EGLRenderLevel.Opaque;

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
