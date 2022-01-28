using System.Text.RegularExpressions;

namespace RtCs.OpenGL
{
    public interface IGLShaderCompileError
    {
        string Filepath { get; }
        int RowNumber { get; }
        string Level { get; }
        int Code { get; }
        string Message { get; }
    }

    public class GLShaderCompileError : IGLShaderCompileError
    {
        public GLShaderCompileError()
        { }

        public static GLShaderCompileError Parse(string inErrorString)
        {
            var regex = new Regex("(?<row>\\d*)\\((?<col>\\d+)\\) : (?<level>warning|error) C(?<code>\\d+): (?<message>.*)");
            var match = regex.Match(inErrorString);

            if (!match.Success) {
                return CreateParseError(inErrorString);
            }

            GLShaderCompileError result = new GLShaderCompileError();
            if (!int.TryParse(match.Groups["row"].Value, out int row)) {
                row = -1;
            }
            result.File = row;
            result.RowNumber = int.Parse(match.Groups["col"].Value);
            result.Level = match.Groups["level"].Value;
            result.Code = int.Parse(match.Groups["code"].Value);
            result.Message = match.Groups["message"].Value;

            return result;
        }

        private static GLShaderCompileError CreateParseError(string inErrorString)
        {
            return new GLShaderCompileError {
                File = -1,
                RowNumber = 0,
                Level = "parse_error",
                Code = 0,
                Message = inErrorString
            };
        }

        public int File
        { get; set; }
        public string Filepath
        { get; set; } = "";
        public int RowNumber
        { get; set; }

        public string Level
        { get; set; }
        public int Code
        { get; set; }
        public string Message
        { get; set; }

        public override string ToString()
        {
            string result = $"({RowNumber}) : {Level} C{Code:d4}: {Message}. {Filepath}";
            if (File >= 0) {
                result = $"{File}{result}";
            }

            return result;
        }
    }
}
