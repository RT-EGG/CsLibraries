using OpenTK.Graphics.OpenGL4;
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
        public class Phong : GLRenderShaderProgram.PresetType
        {
            public Phong()
            {
                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    m_VertexShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.Phong.vertex.glsl.txt"));
                    m_FragmentShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.Phong.fragment.glsl.txt"));

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

                if (inAttributes.TryGetBufferPointerOffset(GLVertexAttribute.AttributeName_Vertex, out int vertex)) {
                    GL.EnableVertexAttribArray(0);
                    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, vertex);
                }
                if (inAttributes.TryGetBufferPointerOffset(GLVertexAttribute.AttributeName_Normal, out int normal)) {
                    GL.EnableVertexAttribArray(1);
                    GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, normal);
                }
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
