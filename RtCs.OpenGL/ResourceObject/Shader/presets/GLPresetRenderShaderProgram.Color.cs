using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using System.Diagnostics;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        /// <summary>
        /// Color shader object as one of preset shader program.
        /// </summary>
        /// <remarks>
        /// This shader renders plane color.
        /// </remarks>
        public class Color : GLRenderShaderProgram.PresetType
        {
            public Color()
            {
                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    GLShaderTextCompiler compiler = new GLShaderTextCompiler();
                    compiler.CompileFromTextResourceFile(m_VertexShader, "RtCs.OpenGL.Resources.Color.vertex.glsl.txt");
                    compiler.CompileFromTextResourceFile(m_FragmentShader, "RtCs.OpenGL.Resources.Color.fragment.glsl.txt");

                    AttachShader(m_VertexShader);
                    AttachShader(m_FragmentShader);
                    Debug.Assert(Link());
                    return;
                };
                return;
            }

            public override void BindVertexAttributes(IGLVertexAttributeList inAttributes)
            {
                base.BindVertexAttributes(inAttributes);

                if (inAttributes.TryGetBufferPointerOffset(GLVertexAttribute.AttributeName_Vertex, out var vertex)) {
                    GL.EnableVertexAttribArray(0);
                    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, vertex);
                }
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
