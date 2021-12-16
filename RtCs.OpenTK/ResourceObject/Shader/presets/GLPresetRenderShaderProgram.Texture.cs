using RtCs.MathUtils;
using System.Collections.Generic;
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
                VertexAttribPointers.Add(new GLVertexAttributePointer.Position(0));
                VertexAttribPointers.Add(new GLVertexAttributePointer.TexCoord(1));

                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    m_VertexShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.Texture.vertex.glsl.txt"));
                    m_FragmentShader.Compile(GLShaderTextSource.CreateAssemblyTextResourceSource($"RtCs.OpenGL.Resources.Texture.fragment.glsl.txt"));

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
