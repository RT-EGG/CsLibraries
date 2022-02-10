using System.Diagnostics;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        /// <summary>
        /// Texture shader object as one of preset shader program.
        /// </summary>
        /// <remarks>
        /// This shader renders using mesh's TexCoord and texture reference.
        /// </remarks>
        public class Texture : GLRenderShaderProgram.PresetType
        {
            public Texture()
            {
                AddAttributePointer(new GLVertexAttributePointer.Float3(0, GLVertexAttribute.AttributeName_Vertex));
                AddAttributePointer(new GLVertexAttributePointer.Float2(1, GLVertexAttribute.AttributeName_TexCoord));

                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    GLShaderTextCompiler compiler = new GLShaderTextCompiler();
                    compiler.CompileFromTextResourceFile(m_VertexShader, "RtCs.OpenGL.Resources.Texture.vertex.glsl.txt");
                    compiler.CompileFromTextResourceFile(m_FragmentShader, "RtCs.OpenGL.Resources.Texture.fragment.glsl.txt");

                    AttachShader(m_VertexShader);
                    AttachShader(m_FragmentShader);
                    Link();
                    Debug.Assert(Linked);
                    return;
                };
                return;
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
