using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.OpenGL.Renderer
{
    public class GLRenderer
    {
        public void Render(GLRenderingStatus inStatus)
        {
            if (Mesh == null) {
                return;
            }
            Mesh.UpdateBuffers();

            var buffers = Mesh.Buffers;
            if ((buffers.Positions == null) || (buffers.Indices == null)) {
                return;
            }
            
            
            try {
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.EnableClientState(ArrayCap.NormalArray);
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffers.Positions);
                GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
                GL.BindBuffer(BufferTarget.ArrayBuffer, buffers.Normals);
                GL.NormalPointer(NormalPointerType.Float, 0, 0);

                GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffers.Indices);

                GL.DrawElements(Mesh.Topology.ToPrimitiveType(), Mesh.Indices.Length, DrawElementsType.UnsignedInt, 0);

            } finally {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            }
        }

        public GLMesh Mesh
        { get; set; } = null;
    }
}
