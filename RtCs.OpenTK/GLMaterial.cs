using OpenTK.Graphics.OpenGL4;
using System;
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

        /// <summary>
        /// Get property by name.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="inName">Name of property.</param>
        /// <exception cref="KeyNotFoundException">Case of that the property not found by name.</exception>
        /// <exception cref="InvalidCastException">Case of that the property is not for type T.</exception>
        /// <returns>The property.</returns>
        public GLShaderUniformProperty<T> GetProperty<T>(string inName)
        {
            var property = m_Properties[inName];
            if (!(property is GLShaderUniformProperty<T>)) {
                throw new InvalidCastException($"");
            }
            return property as GLShaderUniformProperty<T>;
        }

        /// <summary>
        /// Try get property by name.
        /// </summary>
        /// <typeparam name="T">Type of property.</typeparam>
        /// <param name="inName">Name of property.</param>
        /// <param name="outProperty">Output the property if found, otherwise null.</param>
        /// <returns>Return true if the property found by name and type of the property is T, otherwise false.</returns>
        public bool TryGetProperty<T>(string inName, out GLShaderUniformProperty<T> outProperty)
        {
            if (m_Properties.TryGetValue(inName, out var property)) {
                if (property is GLShaderUniformProperty<T>) {
                    outProperty = property as GLShaderUniformProperty<T>;
                    return true;
                }
            }
            outProperty = null;
            return false;
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
