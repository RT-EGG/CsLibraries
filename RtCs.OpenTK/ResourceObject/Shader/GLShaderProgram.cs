﻿using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.OpenGL
{
    /// <summary>
    /// OpenGL shader program object.
    /// </summary>
    public class GLShaderProgram : GLResourceIdObject
    {
        /// <summary>
        /// Attach shader unit as part of program.
        /// </summary>
        /// <param name="inShader">Shader unit object to attach.</param>
        public void AttachShader(GLShader inShader)
            => GL.AttachShader(ID, inShader.ID);
        /// <summary>
        /// Detach shader unit.
        /// </summary>
        /// <param name="inShader">Shader unit object to Detach.</param>
        public void DetachShader(GLShader inShader)
            => GL.DetachShader(ID, inShader.ID);

        /// <summary>
        /// Link shader program using attached shader units.
        /// </summary>
        /// <returns>If compile successed, return true, otherwise false.</returns>
        /// <remarks>
        /// The error will be stored to LinkError when this function failed.
        /// </remarks>
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

        /// <summary>
        /// Lsit of property socket in linked shader program.
        /// </summary>
        /// <remarks>
        /// This will be updated when called Link().
        /// </remarks>
        public IReadOnlyList<GLShaderUniformPropertySocket> UniformPropertySockets
            => m_UniformPropertySockets;
        /// <summary>
        /// Find and get property socket from UniformPropertySockets by name.
        /// </summary>
        /// <param name="inName">Search key equivalent to GLShaderUniformPropertySocket.Name.</param>
        /// <returns>Returns property socket matched with inName, if not found returns null.</returns>
        public GLShaderUniformPropertySocket GetPropertySocket(string inName)
            => UniformPropertySockets.FirstOrDefault(s => s.Name == inName);

        /// <summary>
        /// Create default property for each sockets to initialize.
        /// </summary>
        /// <returns>
        /// Default properties.
        /// </returns>
        public virtual IEnumerable<GLShaderUniformProperty> CreateDefaultProperties()
            => new GLShaderUniformProperty[0];

        protected override void CreateResourceCore()
        {
            base.CreateResourceCore();
            if (ID == 0) {
                ID = GL.CreateProgram();
            }
            return;
        }

        protected override void DestroyResourceCore()
        {
            base.DestroyResourceCore();
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

        /// <summary>
        /// The status the last linking is successed.
        /// </summary>
        public bool Linked => m_LinkState != 0;
        /// <summary>
        /// The error of last linking.
        /// </summary>
        public IReadOnlyList<string> LinkError => m_LinkError;

        /// <summary>
        /// The event be called at the end of Link().
        /// </summary>
        /// <remarks>
        /// This event will be called whether the Link() is successed or failed.
        /// </remarks>
        public event Action<GLShaderProgram> OnAfterLinked;

        private int m_LinkState = 0;
        private List<string> m_LinkError = new List<string>();

        private List<GLShaderUniformPropertySocket> m_UniformPropertySockets = new List<GLShaderUniformPropertySocket>();
    }
}
