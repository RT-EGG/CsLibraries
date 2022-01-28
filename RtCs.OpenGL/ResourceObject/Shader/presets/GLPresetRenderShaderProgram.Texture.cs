﻿using OpenTK.Graphics.OpenGL4;
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
                AfterCreateResource += (sender, args) => {
                    m_VertexShader = new GLShader.GLVertexShader();
                    m_FragmentShader = new GLShader.GLFragmentShader();

                    GLShaderTextCompiler compiler = new GLShaderTextCompiler();
                    compiler.CompileFromTextResourceFile(m_VertexShader, "RtCs.OpenGL.Resources.Texture.vertex.glsl.txt");
                    compiler.CompileFromTextResourceFile(m_FragmentShader, "RtCs.OpenGL.Resources.Texture.fragment.glsl.txt");

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
                if (inAttributes.TryGetBufferPointerOffset(GLVertexAttribute.AttributeName_TexCoord, out int texCoord)) {
                    GL.EnableVertexAttribArray(1);
                    GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, sizeof(float) * 2, texCoord);
                }
            }

            private GLShader.GLVertexShader m_VertexShader = null;
            private GLShader.GLFragmentShader m_FragmentShader = null;
        }
    }
}
