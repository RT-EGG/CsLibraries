using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public interface IGLBlendParameters
    {
        void Apply();
    }

    public class GLBlendParameters : IGLBlendParameters
    {
        public void Apply()
            => GL.BlendFunc(SourceFactor, DestinationFactor);

        public BlendingFactor SourceFactor
        { get; set; } = BlendingFactor.One;
        public BlendingFactor DestinationFactor
        { get; set; } = BlendingFactor.Zero;

        public static GLBlendParameters Default = new GLBlendParameters {
            SourceFactor = BlendingFactor.One,
            DestinationFactor = BlendingFactor.Zero
        };
    }
}
