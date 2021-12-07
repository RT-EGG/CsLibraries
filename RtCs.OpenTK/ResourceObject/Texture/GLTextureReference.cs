namespace RtCs.OpenGL
{
    public class GLTextureReference
    {
        public IGLTexture Texture
        { get; set; } = null;
        public GLTextureSampler Sampler
        { get; set; } = null;
    }
}
