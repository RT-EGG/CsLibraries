namespace RtCs.OpenGL
{
    class GLShaderCompileIncludeFileError : GLShaderCompileIncludeDirectiveError
    {
        public GLShaderCompileIncludeFileError(string inReadingFilepath, int inRowNumber, string inIncludeFile)
            : base(inReadingFilepath, inRowNumber, $"Include file not found \"{inIncludeFile}\".")
        {
            IncludeFile = inIncludeFile;
        }

        public readonly string IncludeFile;
    }
}
