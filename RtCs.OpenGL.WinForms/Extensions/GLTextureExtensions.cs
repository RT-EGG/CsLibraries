using System.Drawing;

namespace RtCs.OpenGL
{
    public static class GLTextureExtensions
    {
        public static Size GetSize(this IGLTexture2D inTexture)
            => new Size(inTexture.Width, inTexture.Height);
    }
}
