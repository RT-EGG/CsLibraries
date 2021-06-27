using OpenTK.Graphics.OpenGL4;
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

        public void CommitProperty(GLShaderProgram inShader)
            => DoCommitProperty(inShader.ID, Location);

        protected abstract void DoCommitProperty(int inProgramID, int inLocation);

        public readonly GLShaderUniformPropertySocket Socket;

        public class Int : GLShaderUniformProperty<int>
        {
            public Int(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Double : GLShaderUniformProperty<double>
        {
            public Double(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation)
                => GL.ProgramUniform1(inProgramID, inLocation, Value);
        }

        public class Vec4 : GLShaderUniformProperty<Vector4>
        {
            public Vec4(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation)
                => GL.ProgramUniform4(inProgramID, inLocation, 1, Value.Select(v => (float)v).ToArray());
        }

        public class Mat4 : GLShaderUniformProperty<Matrix4x4>
        {
            public Mat4(GLShaderUniformPropertySocket inSocket) : base(inSocket) { }
            protected override void DoCommitProperty(int inProgramID, int inLocation)
                => GL.ProgramUniformMatrix4(inProgramID, inLocation, 1, false, Value.ToGLArrayF());
        }
    }

    public abstract class GLShaderUniformProperty<T> : GLShaderUniformProperty
    {
        public GLShaderUniformProperty(GLShaderUniformPropertySocket inSocket)
            : base (inSocket) { }

        public T Value
        { get; set; } = default;
    }
}
