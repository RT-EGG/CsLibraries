﻿using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public abstract class GLShaderUniformProperty<T>
    {
        public abstract void CommitProperty(int inProgramID);

        public string Name
        { get; set; } = "";

        public T Value
        { get; set; } = default;

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
    }
}
