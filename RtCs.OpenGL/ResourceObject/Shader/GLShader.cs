using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    /// <summary>
    /// OpenGL shader unit object.
    /// </summary>
    /// <remarks>
    /// This class is abstract so don't use directlly.\n
    /// Use classes inherit this class (GLShader.GLVertexShader, GLShader.GLFragmentShader, GLShader.GLTessControlShader, GLShader.GLTessEvaluationShader or GLShader.GLComputeShader).
    /// </remarks>
    public abstract partial class GLShader : GLResourceIdObject
    {
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

        /// <summary>
        /// The status the last compilation is successed.
        /// </summary>
        public bool Compiled
        { get; internal set; } = false;
        //public bool Compiled => m_CompileState != 0;
        /// <summary>
        /// The error of last compilation.
        /// </summary>
        public IReadOnlyList<GLShaderCompileError> CompileError
        { get; internal set; } = new List<GLShaderCompileError>();

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

            /// <summary>
            /// 
            /// </summary>
            /// <remarks>
            /// The value may be not correct value until initialize opengl context.
            /// </remarks>
            //public static int MaxTessGenLevel => GLProperties.MaxTessGenLeven;
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
