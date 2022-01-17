using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RtCs.OpenGL
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    class GLShaderBuiltInVariableAttribute : Attribute
    {
        public string Name = "";
        public ActiveUniformType UniformType = (ActiveUniformType)(-1);
    }

    public enum EGLShaderBuiltInVariable
    {
        [GLShaderBuiltInVariable(Name = "ModelMatrix", UniformType = ActiveUniformType.FloatMat4)]
        ModelMatrix,
        [GLShaderBuiltInVariable(Name = "ViewMatrix", UniformType = ActiveUniformType.FloatMat4)]
        ViewMatrix,
        [GLShaderBuiltInVariable(Name = "ProjectionMatrix", UniformType = ActiveUniformType.FloatMat4)]
        ProjectionMatrix,
        [GLShaderBuiltInVariable(Name = "ModelViewMatrix", UniformType = ActiveUniformType.FloatMat4)]
        ModelViewMatrix,
        [GLShaderBuiltInVariable(Name = "ViewProjectionMatrix", UniformType = ActiveUniformType.FloatMat4)]
        ViewProjectionMatrix,
        [GLShaderBuiltInVariable(Name = "ModelViewProjectionMatrix", UniformType = ActiveUniformType.FloatMat4)]
        ModelViewProjectionMatrix,
        [GLShaderBuiltInVariable(Name = "NormalMatrix", UniformType = ActiveUniformType.FloatMat3)]
        NormalMatrix
    }

    public static class GLShaderBuiltInVariable
    {
        static GLShaderBuiltInVariable()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("RtCs.OpenGL.Resources.ShaderBuiltInSource.h.txt"))) {
                m_BuiltinSourceCode = reader.ReadToEnd();
            }
            return;
        }

        public static string GetName(this EGLShaderBuiltInVariable inValue)
            => inValue.GetAttribute().Name;

        public static ActiveUniformType GetUniformType(this EGLShaderBuiltInVariable inValue)
            => inValue.GetAttribute().UniformType;

        private static GLShaderBuiltInVariableAttribute GetAttribute(this EGLShaderBuiltInVariable inValue)
        {
            var type = typeof(EGLShaderBuiltInVariable);
            var name = Enum.GetName(type, inValue);
            return type.GetField(name).GetCustomAttributes(false).Cast<GLShaderBuiltInVariableAttribute>().FirstOrDefault();
        }

        internal static string IncludeBuiltIn(string inText)
        {
            const string IncludeLine = "#include \"BuiltIn.h\"";
            return inText.Replace(IncludeLine, BuiltInSourceCode);
        }
        internal static string BuiltInIncludeHeader => "BuiltIn.h";
        internal static string BuiltInSourceCode => m_BuiltinSourceCode;

        private static readonly string m_BuiltinSourceCode;
    }
}
