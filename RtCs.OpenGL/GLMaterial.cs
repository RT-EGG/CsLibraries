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

        public void CommitProperties()
            => CommitPropertiesCore();

        protected virtual void CommitPropertiesCore()
        {
            if (Shader == null) {
                return;
            }

            GLShaderUniformVariable.CommitStatus commitState = new GLShaderUniformVariable.CommitStatus {
                CurrentAvailableTextureUnit = TextureUnit.Texture0
            };
            foreach (var variable in m_UniformVariables.Values) {
                variable.CommitVariable(Shader, commitState);
            }
            return;
        }

        /// <summary>
        /// Get property by name.
        /// </summary>
        /// <typeparam name="T">Type of the variable.</typeparam>
        /// <param name="inName">Name of the variable.</param>
        /// <exception cref="KeyNotFoundException">Case of that the variable not found by name.</exception>
        /// <exception cref="InvalidCastException">Case of that the variable is not for type T.</exception>
        /// <returns>The variable.</returns>
        public GLShaderUniformVariable<T> GetVariable<T>(string inName)
        {
            var property = m_UniformVariables[inName];
            if (!(property is GLShaderUniformVariable<T>)) {
                throw new InvalidCastException($"The property \"{inName}\" is not for {typeof(T).Name}.");
            }
            return property as GLShaderUniformVariable<T>;
        }

        /// <summary>
        /// Try get variable by name.
        /// </summary>
        /// <typeparam name="T">Type of the variable.</typeparam>
        /// <param name="inName">Name of variable.</param>
        /// <param name="outProperty">Output the property if found, otherwise null.</param>
        /// <returns>Return true if the variable found by name and type of the property is T, otherwise false.</returns>
        public bool TryGetVariable<T>(string inName, out GLShaderUniformVariable<T> outProperty)
        {
            if (m_UniformVariables.TryGetValue(inName, out var property)) {
                if (property is GLShaderUniformVariable<T>) {
                    outProperty = property as GLShaderUniformVariable<T>;
                    return true;
                }
            }
            outProperty = null;
            return false;
        }

        /// <summary>
        /// Set property value.
        /// </summary>
        /// <typeparam name="T">Type of the variable.</typeparam>
        /// <param name="inName">Name of the variable.</param>
        /// <param name="inValue">Value of the variable.</param>
        /// <remarks>
        /// If not found variable inName, or the variable is not T, do nothing.
        /// </remarks>
        public void SetVariableValue<T>(string inName, T inValue)
        {
            var variable = GetVariable<T>(inName);
            if (variable != null) {
                variable.Value = inValue;
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
                    m_Shader.AfterLinked -= OnShaderLinked;
                }

                m_UniformVariables.Clear();
                m_Shader = value;
                if (m_Shader != null) {
                    m_Shader.AfterLinked += OnShaderLinked;
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
            Dictionary<string, GLShaderUniformVariable> newList = new Dictionary<string, GLShaderUniformVariable>();
            if (m_Shader != null) {
                // Inherit from previous properties.
                foreach (var socket in m_Shader.UniformVariableSockets) {
                    if (m_UniformVariables.TryGetValue(socket.Name, out var property)) {
                        newList.Add(socket.Name, property);
                    }
                }

                foreach (var defaultProperty in m_Shader.CreateDefaultUniformVariable()) {
                    if (defaultProperty?.Socket == null) {
                        continue;
                    }
                    if (!newList.ContainsKey(defaultProperty.Name)) {
                        newList.Add(defaultProperty.Name, defaultProperty);
                    } else {
                        newList[defaultProperty.Name] = defaultProperty;
                    }
                }
            }

            m_UniformVariables = newList;
            return;
        }

        private Dictionary<string, GLShaderUniformVariable> m_UniformVariables = new Dictionary<string, GLShaderUniformVariable>();
        private GLRenderShaderProgram m_Shader = null;
    }
}
