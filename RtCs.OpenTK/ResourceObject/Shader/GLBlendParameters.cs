using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public interface IGLBlendParameters
    {
        void Apply();
    }

    /// <summary>
    /// The parameter of blending.
    /// </summary>
    public class GLBlendParameters : IGLBlendParameters
    {
        public GLBlendParameters()
            : this (BlendingFactor.One, BlendingFactor.Zero)
        { }

        public GLBlendParameters(BlendingFactor inSourceFactor, BlendingFactor inDestinationFactor)
        {
            SourceFactor = inSourceFactor;
            DestinationFactor = inDestinationFactor;
            return;
        }

        public void Apply()
            => GL.BlendFunc(SourceFactor, DestinationFactor);

        /// <summary>
        /// The parameter for glBlendFunc 1st argument.
        /// </summary>
        public BlendingFactor SourceFactor
        { get; set; } = BlendingFactor.One;
        /// <summary>
        /// The parameter for glBlendFunc 2nd argument.
        /// </summary>
        public BlendingFactor DestinationFactor
        { get; set; } = BlendingFactor.Zero;

        public static GLBlendParameters Default = new GLBlendParameters();
    }
}
