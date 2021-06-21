using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System.Linq;

namespace RtCs.OpenGL
{
    public abstract class GLShaderUniformProperty
    {
        public string Name
        { get; set; } = "";

        public abstract void CommitProperty(int inProgramID);

        protected int GetUniformLocation(int inProgramID)
            => GL.GetUniformLocation(inProgramID, Name);

        public class Int : GLShaderUniformProperty<int>
        {
            public override void CommitProperty(int inProgramID)
                => GL.ProgramUniform1(inProgramID, GetUniformLocation(inProgramID), Value);
        }

        public class Double : GLShaderUniformProperty<double>
        {
            public override void CommitProperty(int inProgramID)
                => GL.ProgramUniform1(inProgramID, GetUniformLocation(inProgramID), Value);
        }

        public class Vec4 : GLShaderUniformProperty<Vector4>
        {
            public override void CommitProperty(int inProgramID)
                => GL.ProgramUniform4(inProgramID, GetUniformLocation(inProgramID), 1, Value.Select(v => (float)v).ToArray());
        }

        public class Mat4 : GLShaderUniformProperty<Matrix4x4>
        {
            public override void CommitProperty(int inProgramID)
                => GL.ProgramUniformMatrix4(inProgramID, GetUniformLocation(inProgramID), 1, false, Value.ToGLArrayF());
        }
    }

    public abstract class GLShaderUniformProperty<T> : GLShaderUniformProperty
    {
        public T Value
        { get; set; } = default;
    }
}
