using System;

namespace RtCs.OpenGL
{
    class GLShaderCompileIncludeDirectiveError : FormatException
    {
        public GLShaderCompileIncludeDirectiveError(string inReadingFilepath, int inRowNumber, string inMessage)
            : base(inMessage)
        {
            ReadingFilepath = inReadingFilepath;
            RowNumber = inRowNumber;
        }

        public readonly string ReadingFilepath;
        public readonly int RowNumber;
    }
}
