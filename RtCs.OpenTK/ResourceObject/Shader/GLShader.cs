using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    public abstract partial class GLShader : GLResourceIdObject
    {
        public bool Compile(GLShaderSource inSource)
        {
            m_CompileError.Clear();
            if (ID == 0) {
                m_CompileError.Add("Shader object has not created.");
                return false;
            }

            if (inSource == null) {
                m_CompileError.Add("Shader source has not been ready.");
                return false;
            }

            inSource.LoadSource(ID);
            GL.CompileShader(ID);

            GL.GetShader(ID, ShaderParameter.CompileStatus, out m_CompileState);
            if (!Compiled) {
                m_CompileError.AddRange(GL.GetShaderInfoLog(ID).Split('\n'));
            }
            return Compiled;
        }

        protected override void CreateResourceCore()
        {
            base.CreateResourceCore();
            if (ID == 0) {
                ID = GL.CreateShader(ShaderType);
            }
            return;
        }

        protected override void DestroyResourceCore()
        {
            base.DestroyResourceCore();
            if (ID != 0) {
                GL.DeleteShader(ID);
                ID = 0;
            }
            return;
        }

        public abstract ShaderType ShaderType { get; }

        public bool Compiled => m_CompileState != 0;
        public IReadOnlyList<string> CompileError => m_CompileError;

        private int m_CompileState = 0;
        private List<string> m_CompileError = new List<string>();

        public class GLVertexShader : GLShader
        {
            public override ShaderType ShaderType => ShaderType.VertexShader;
        }

        public class GLFragmentShader : GLShader
        {
            public override ShaderType ShaderType => ShaderType.FragmentShader;
        }

        public class GLTessControlShader : GLShader
        {
            public override ShaderType ShaderType => ShaderType.TessControlShader;
        }

        public class GLTessEvaluationShader : GLShader
        {
            public override ShaderType ShaderType => ShaderType.TessEvaluationShader;
        }

        public class GLComputeShader : GLShader
        {
            public override ShaderType ShaderType => ShaderType.ComputeShader;
        }
    }

    
}
