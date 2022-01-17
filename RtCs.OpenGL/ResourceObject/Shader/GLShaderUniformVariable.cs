using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System.Linq;

namespace RtCs.OpenGL
{
    public abstract class GLShaderUniformVariable
    {
        public GLShaderUniformVariable(GLShaderUniformVariableSocket inSocket)
        {
            Socket = inSocket;
            return; 
        }

        public string Name => Socket.Name;
        public int Location => Socket.Location;

        public void CommitVariable(GLShaderProgram inShader, CommitStatus inCommitState)
            => CommitVariableCore(inShader.ID, Location, inCommitState);

        protected abstract void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState);

        public readonly GLShaderUniformVariableSocket Socket;

        public class CommitStatus
        {
            public TextureUnit CurrentAvailableTextureUnit
            { get; set; } = TextureUnit.Texture0;
        }

        public class Bool : GLShaderUniformVariable<bool>
        {
            public Bool(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform1(inProgramID, inLocation, Value ? 1 : 0);
        }

        public class BoolVec3 : GLShaderUniformVariable<Container3<bool>>
        {
            public BoolVec3(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform3(inProgramID, inLocation, Value[0] ? 1 : 0, Value[1] ? 1 : 0, Value[2] ? 1 : 0);
        }

        public class Int : GLShaderUniformVariable<int>
        {
            public Int(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Float : GLShaderUniformVariable<float>
        {
            public Float(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Double : GLShaderUniformVariable<double>
        {
            public Double(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Vec3 : GLShaderUniformVariable<Vector3>
        {
            public Vec3(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform3(inProgramID, inLocation, 1, Value.ToArray());
        }

        public class Vec4 : GLShaderUniformVariable<Vector4>
        {
            public Vec4(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform4(inProgramID, inLocation, 1, Value.ToArray());
        }

        public class Mat4 : GLShaderUniformVariable<Matrix4x4>
        {
            public Mat4(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniformMatrix4(inProgramID, inLocation, 1, false, Value.ToGLFloatArray());
        }

        public class Texture : GLShaderUniformVariable<GLTextureReference>
        {
            public Texture(GLShaderUniformVariableSocket inSocket) : base(inSocket) { }
            protected override void CommitVariableCore(int inProgramID, int inLocation, CommitStatus inCommitState)
            {
                int unit = inCommitState.CurrentAvailableTextureUnit - TextureUnit.Texture0;
                GL.BindTextureUnit(unit, Value.Texture.ID);
                GL.BindSampler(unit, Value.Sampler);
                GL.ProgramUniform1(inProgramID, inLocation, unit);

                inCommitState.CurrentAvailableTextureUnit++;
                return;
            }
        }
    }

    /// <summary>
    /// Property value object for shader program.
    /// </summary>
    /// <remarks>
    /// The instance is created material instance for each shader property socket.
    /// </remarks>
    public abstract class GLShaderUniformVariable<T> : GLShaderUniformVariable
    {
        public GLShaderUniformVariable(GLShaderUniformVariableSocket inSocket)
            : base (inSocket) { }

        /// <summary>
        /// Get or set value to commit shader program.
        /// </summary>
        public T Value
        { get; set; } = default;
    }
}
