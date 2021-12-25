using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    /// <summary>
    /// The object to render from mesh and material.
    /// </summary>
    public class GLRenderer : GLObject
    {
        ///// <summary>
        ///// Execute render command.
        ///// </summary>
        ///// <param name="inParameter">Rendering data store.</param>
        //public void Render(GLRenderParameter inParameter)
        //{
        //    if (Mesh == null) {
        //        return;
        //    }

        //    var shader = Material?.Shader;
        //    if (shader == null) {
        //        return;
        //    }

        //    if (!shader.Linked) {
        //        if (!shader.Link()) {
        //            return;
        //        }
        //    }
        //    try {
        //        GL.UseProgram(shader.ID);
        //        Material.CommitProperties(inParameter);

        //        GL.BindBuffer(BufferTarget.ArrayBuffer, Mesh.VertexBuffer);

        //        foreach (var attribPointer in shader.VertexAttribPointers) {
        //            var attrib = Mesh.VertexAttributes[attribPointer.AttributeType];
        //            if (attrib == null) {
        //                continue;
        //            }
        //            GL.EnableVertexAttribArray(attribPointer.Index);
        //            GL.VertexAttribPointer(attribPointer.Index, attrib.Size, VertexAttribPointerType.Float, false, attrib.Stride, attrib.Offset);
        //        }

        //        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Mesh.IndexBuffer);
        //        GL.DrawElements(Mesh.Topology.ToPrimitiveType(), Mesh.Indices.Length, DrawElementsType.UnsignedInt, 0);

        //    } finally {
        //        GL.UseProgram(0);
        //        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        //        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        //    }
        //}

        /// <summary>
        /// Get or set mesh.
        /// </summary>
        public GLMesh Mesh
        { get; set; } = null;
        /// <summary>
        /// Get or set material.
        /// </summary>
        public GLMaterial Material
        { get; set; } = null;
    }
}
