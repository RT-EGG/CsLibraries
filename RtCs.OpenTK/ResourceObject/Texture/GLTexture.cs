using OpenTK.Graphics.OpenGL4;

namespace RtCs.OpenGL
{
    public interface ITexture
    {
        PixelInternalFormat PixelInternalFormat { get; }
    }

    public interface IColorTexture : ITexture
    {
        ColorRGBA[] Pixels { get; }
    }

    public interface ITexture2D : ITexture
    {
        int Width { get; }
        int Height { get; }
    }

    public abstract class GLTexture : GLResourceIdObject, ITexture
    {
        public GLTexture(PixelInternalFormat inPixelInternalFormat)
        {
            PixelInternalFormat = inPixelInternalFormat;
            return;
        }

        // sampler is not here
        //public GLTextureSampler Samlper
        //{ get; } = new GLTextureSampler();

        public virtual void Apply()
        { }

        public PixelInternalFormat PixelInternalFormat
        { get; }

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

    public abstract class GLColorTexture : GLTexture, IColorTexture
    {
        public GLColorTexture()
            : base(PixelInternalFormat.Rgba)
        { }

        public override void Apply()
        {
            base.Apply();
            LoadPixels(Pixels);
        }

        public ColorRGBA[] Pixels
        { get; set; } = new ColorRGBA[0];

        protected abstract void LoadPixels(ColorRGBA[] inPixels);
    }

    public class GLColorTexture2D : GLColorTexture, ITexture2D
    {
        public GLColorTexture2D(int inWidth, int inHeight)
            : base()
        {
            Width = inWidth;
            Height = inHeight;
            return;
        }

        public int Width
        { get; private set; } = 0;
        public int Height
        { get; private set; } = 0;

        protected override void LoadPixels(ColorRGBA[] inPixels)
        {
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, ID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, inPixels);
            return;
        }
    }
}
