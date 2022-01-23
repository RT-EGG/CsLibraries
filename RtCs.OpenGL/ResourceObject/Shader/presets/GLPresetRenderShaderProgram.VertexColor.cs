using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;

namespace RtCs.OpenGL
{
    public partial class GLRenderShaderProgram
    {
        /// <summary>
        /// VertexColor shader object as one of preset shader program.
        /// </summary>
        /// <remarks>
        /// This shader renders using mesh's vertex color. The fragment color will be deciced from interpolated between vertices.
        /// </remarks>
        public class VertexColor : GLRenderShaderProgram.PresetType
        {
            public VertexColor()
            {
                VertexAttribPointers.Add(new GLVertexAttributePointer(0, GLVertexAttribute.AttributeName_Vertex));
                VertexAttribPointers.Add(new GLVertexAttributePointer(1, GLVertexAttribute.AttributeName_Color));

                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    m_VertexShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.VertexColor.vertex.glsl.txt"));
                    m_FragmentShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.VertexColor.fragment.glsl.txt"));

                    AttachShader(m_VertexShader);
                    AttachShader(m_FragmentShader);
                    Debug.Assert(Link());
                    return;
                };
                return;
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
