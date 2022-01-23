using OpenTK.Graphics.OpenGL4;
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
                m_UniformBlockSockets = CollectUniformBlockSockets(ID, m_UniformPropertySockets);
            } else {
                m_UniformPropertySockets.Clear();
                m_UniformBlockSockets.Clear();
                m_LinkError.AddRange(GL.GetProgramInfoLog(ID).Split('\n'));
            }
            AfterLinked?.Invoke(this);
            return Linked;
        }

        /// <summary>
        /// Lsit of property socket in linked shader program.
        /// </summary>
        /// <remarks>
        /// This will be updated when called Link().
        /// </remarks>
        public IReadOnlyList<GLShaderUniformVariableSocket> UniformVariableSockets
            => m_UniformPropertySockets;

        /// <summary>
        /// Lsit of uniform block socket in linked shader program.
        /// </summary>
        /// <remarks>
        /// This will be updated when called Link().
        /// </remarks>
        public IReadOnlyList<GLShaderUniformBlockSocket> UniformBlockSockets
            => m_UniformBlockSockets;

        /// <summary>
        /// Get binding-point of shader-storage-buffer-object in shader program.
        /// </summary>
        /// <param name="inName">The name of buffer.</param>
        /// <returns>Returns binding-point if found by name, otherwise -1.</returns>
        public int GetShaderStorageBufferBindPoint(string inName)
        {
            if (m_ShaderStorageBufferSockets.TryGetFirst(out var socket, s => s.Name == inName)) {
                return socket.Binding;
            }

            var binding = GL.GetProgramResourceIndex(ID, ProgramInterface.ShaderStorageBlock, inName);
            if (binding < 0) {
                return -1;
            }

            var newSocket = new GLShaderStorageBufferSocket(inName, binding);
            m_ShaderStorageBufferSockets.Add(newSocket);
            return binding;
        }

        /// <summary>
        /// Get binding-point of shader-storage-buffer-object in shader program.
        /// </summary>
        /// <param name="inName">The name of buffer.</param>
        /// <param name="outBinding">The binding-point of buffer.</param>
        /// <returns>Returns true if found by name, otherwise false.</returns>
        public bool TryGetShaderStorageBufferBindPoint(string inName, out int outBinding)
        {
            outBinding = GetShaderStorageBufferBindPoint(inName);
            return outBinding >= 0;
        }

        /// <summary>
        /// Find and get property socket from UniformPropertySockets by name.
        /// </summary>
        /// <param name="inName">Search key equivalent to GLShaderUniformPropertySocket.Name.</param>
        /// <returns>Returns property socket matched with inName, if not found returns null.</returns>
        public GLShaderUniformVariableSocket GetUniformVariableSocket(string inName)
            => UniformVariableSockets.FirstOrDefault(s => s.Name == inName);

        /// <summary>
        /// Create default property for each sockets to initialize.
        /// </summary>
        /// <returns>
        /// Default properties.
        /// </returns>
        public virtual IEnumerable<GLShaderUniformVariable> CreateDefaultUniformVariable()
            => UniformVariableSockets.Select(s => s.CreateDefaultProperty()).Where(s => s != null);

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

        protected static List<GLShaderUniformVariableSocket> CollectUniformPropertySockets(int inProgramID)
        {
            GL.GetProgram(inProgramID, GetProgramParameterName.ActiveUniforms, out int count);
            var result = new List<GLShaderUniformVariableSocket>(count);
            
            int size, length;            
            for (int i = 0; i < count; ++i) {
                GL.GetActiveUniform(inProgramID, i, 255, out length, out size, out ActiveUniformType type, out string name);
                int location = GL.GetUniformLocation(inProgramID, name);

                result.Add(new GLShaderUniformVariableSocket(name, location, type));
            }           

            return result;
        }

        protected static List<GLShaderUniformBlockSocket> CollectUniformBlockSockets(int inProgramID, IReadOnlyList<GLShaderUniformVariableSocket> inUniformSockets)
        {
            GL.GetProgram(inProgramID, GetProgramParameterName.ActiveUniformBlocks, out int count);
            var result = new List<GLShaderUniformBlockSocket>(count);

            int length;
            for (int i = 0; i < count; ++i) {
                GL.GetActiveUniformBlockName(inProgramID, i, 255, out length, out string name);
                GL.GetActiveUniformBlock(inProgramID, i, ActiveUniformBlockParameter.UniformBlockBinding, out int binding);
                GL.GetActiveUniformBlock(inProgramID, i, ActiveUniformBlockParameter.UniformBlockDataSize, out int dataSize);
                GL.GetActiveUniformBlock(inProgramID, i, ActiveUniformBlockParameter.UniformBlockActiveUniforms, out int numUniforms);

                int[] indices = new int[numUniforms];
                GL.GetActiveUniformBlock(inProgramID, i, ActiveUniformBlockParameter.UniformBlockActiveUniformIndices, indices);

                result.Add(new GLShaderUniformBlockSocket(name, binding, dataSize, inUniformSockets.ElementsAt(indices).ToArray()));
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
        public event Action<GLShaderProgram> AfterLinked;

        private int m_LinkState = 0;
        private List<string> m_LinkError = new List<string>();

        private List<GLShaderUniformVariableSocket> m_UniformPropertySockets = new List<GLShaderUniformVariableSocket>();
        private List<GLShaderUniformBlockSocket> m_UniformBlockSockets = new List<GLShaderUniformBlockSocket>();
        private List<GLShaderStorageBufferSocket> m_ShaderStorageBufferSockets = new List<GLShaderStorageBufferSocket>();
    }
}
