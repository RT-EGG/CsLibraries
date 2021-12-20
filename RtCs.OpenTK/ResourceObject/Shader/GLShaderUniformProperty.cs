﻿using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System.Linq;

namespace RtCs.OpenGL
{
    public abstract class GLShaderUniformProperty
    {
        public GLShaderUniformProperty(GLShaderUniformPropertySocket inSocket)
        {
            Socket = inSocket;
            return; 
        }

        public string Name => Socket.Name;
        public int Location => Socket.Location;

        public void CommitProperty(GLShaderProgram inShader, CommitStatus inCommitState)
            => DoCommitProperty(inShader.ID, Location, inCommitState);

        protected abstract void DoCommitProperty(int inProgramID, int inLocation, CommitStatus inCommitState);

        public readonly GLShaderUniformPropertySocket Socket;

        public class CommitStatus
        {
            public TextureUnit CurrentAvailableTextureUnit
            { get; set; } = TextureUnit.Texture0;
        }

        public class Int : GLShaderUniformProperty<int>
        {
            public Int(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Float : GLShaderUniformProperty<float>
        {
            public Float(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Double : GLShaderUniformProperty<double>
        {
            public Double(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Vec4 : GLShaderUniformProperty<Vector4>
        {
            public Vec4(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniform4(inProgramID, inLocation, 1, Value.Select(v => (float)v).ToArray());
        }

        public class Mat4 : GLShaderUniformProperty<Matrix4x4>
        {
            public Mat4(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation, CommitStatus inCommitState)
                => GL.ProgramUniformMatrix4(inProgramID, inLocation, 1, false, Value.ToGLFloatArray());
        }

        public class Texture : GLShaderUniformProperty<GLTextureReference>
        {
            public Texture(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation, CommitStatus inCommitState)
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
    public abstract class GLShaderUniformProperty<T> : GLShaderUniformProperty
    {
        public GLShaderUniformProperty(GLShaderUniformPropertySocket inSocket)
            : base (inSocket) { }

        /// <summary>
        /// Get or set value to commit shader program.
        /// </summary>
        public T Value
        { get; set; } = default;
    }
}
