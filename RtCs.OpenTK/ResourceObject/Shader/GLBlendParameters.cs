using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public interface IGLBlendParameters
    {
        void Apply();
    }

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

        public BlendingFactor SourceFactor
        { get; set; } = BlendingFactor.One;
        public BlendingFactor DestinationFactor
        { get; set; } = BlendingFactor.Zero;

        public static GLBlendParameters Default = new GLBlendParameters();
    }
}
