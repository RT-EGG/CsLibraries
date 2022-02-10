using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;

namespace RtCs.OpenGL
{
    class GLMeshRenderer : GLObject
    {
        public GLMeshRenderer(GLMesh inMesh, GLRenderShaderProgram inShader)
        {
            Mesh = inMesh;
            Shader = inShader;

            Mesh.AfterBufferUpdate += (sender, args) => Record();
            Shader.AfterLinked += _ => Record();

            Record();
        }

        public void Record()
        {
            if (RecordRequested) {
                return;
            }

            RecordRequested = true;
            GLMainThreadTask.CreateNew(_ => {
                IndexCount = Mesh.Indices.Length;

                m_VAO.Bind();

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, Mesh.IndexBuffer);
                GL.BindBuffer(BufferTarget.ArrayBuffer, Mesh.VertexBuffer);

                Dictionary<string, VertexBufferBindingPoint> bindings = new Dictionary<string, VertexBufferBindingPoint>();

                int index = 0;
                foreach (var pointer in Shader.VertexAttributePointers) {
                    if (!bindings.TryGetValue(pointer.AttributeName, out var binding)) {
                        if (Mesh.VertexAttributes.TryGetFirst(out var attribute, a => a.Descriptors.Contains(d => d.Name == pointer.AttributeName))) {
                            int descOffset = 0;
                            foreach (var desc in attribute.Descriptors) {
                                var p = new VertexBufferBindingPoint(index, descOffset);
                                bindings.Add(desc.Name, p);
                                descOffset += desc.Size;

                                if (desc.Name == pointer.AttributeName) {
                                    binding = p;
                                }
                            }

                            GL.VertexArrayVertexBuffer(m_VAO.ID, index++, Mesh.VertexBuffer, (IntPtr)attribute.Offset, attribute.Stride);
                        }
                    }

                    if (binding == null) {
                        continue;
                    }

                    GL.EnableVertexArrayAttrib(m_VAO.ID, pointer.Location);
                    GL.VertexArrayAttribBinding(m_VAO.ID, pointer.Location, binding.BindingIndex);
                    pointer.BindFormat(m_VAO, binding.OffsetInAttribute);
                }

                GL.BindVertexArray(0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

                RecordRequested = false;
            },
            new GLMainThreadTaskOptions {
                ExecuteIfCanSoon = false
            });
        }

        public void Render()
        {
            m_VAO.Bind();
            GL.DrawElements(Mesh.Topology.ToPrimitiveType(), IndexCount, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                m_VAO.Dispose();
            }
        }

        public readonly GLMesh Mesh;
        public readonly GLRenderShaderProgram Shader;

        private bool RecordRequested = false;
        private GLVertexArrayObject m_VAO = new GLVertexArrayObject();
        private int IndexCount { get; set; } = 0;

        private class VertexBufferBindingPoint
        {
            public VertexBufferBindingPoint(int inBindingIndex, int inOffsetInAttribute)
            {
                BindingIndex = inBindingIndex;
                OffsetInAttribute = inOffsetInAttribute;
            }

            public readonly int BindingIndex;
            public readonly int OffsetInAttribute;
        }
    }
}
