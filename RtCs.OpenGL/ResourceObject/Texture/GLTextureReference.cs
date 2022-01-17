namespace RtCs.OpenGL
{
    /// <summary>
    /// Set of texture object and texture sampler object that tells how to sample the texture.
    /// </summary>
    public class GLTextureReference
    {
        /// <summary>
        /// Get or set texture object.
        /// </summary>
        public IGLTexture Texture
        { get; set; } = null;
        /// <summary>
        /// Get or set texture sampler object.
        /// </summary>
        public GLTextureSampler Sampler
        { get; set; } = null;
    }
}
