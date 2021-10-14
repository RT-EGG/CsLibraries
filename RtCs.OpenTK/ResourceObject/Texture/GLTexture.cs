using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.OpenGL
{
    public class GLTexture : GLResourceIdObject
    {
        public GLTextureSampler Samlper
        { get; } = new GLTextureSampler();

        protected override void InternalCreateResource()
        {
            base.InternalCreateResource();
            ID = GL.GenTexture();
            return;
        }

        protected override void InternalDestroyResource()
        {
            base.InternalDestroyResource();
            GL.DeleteTexture(ID);
            ID = 0;
            return;
        }
    }
}
