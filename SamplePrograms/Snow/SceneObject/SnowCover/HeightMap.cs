using OpenTK.Graphics.OpenGL4;
using RtCs.MathUtils;
using RtCs.OpenGL;
using System;

namespace Snow.SceneObject.SnowCover
{
    class HeightMap : GLTexture, IGLTexture2D
    {
        public HeightMap()
            : base(PixelInternalFormat.Rgba32f)
        {
        }

        protected override void DisposeObject(bool inDisposing)
        {
            base.DisposeObject(inDisposing);

            if (inDisposing) {
                m_Randomizer.Dispose();
                m_Randomizer = null;
            }
        }

        public int Width => m_Width;
        public int Height => m_Height;
        public Vector2 Size => new Vector2(Width, Height);

        public void Randomize(int inTextureWidth, int inTextureHeight, float inSeed)
        {
            m_Width = inTextureWidth;
            m_Height = inTextureHeight;

            GLMainThreadTask.CreateNew(_ => {
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, ID);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat, Width, Height, 0, PixelFormat.Rgba, PixelType.Float, (IntPtr)null);

                // TextureMinFilter must be initialized to used by ComputeShader...
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new int[] { (int)All.Nearest });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new int[] { (int)All.Linear });

                m_Randomizer.Execute(this, inSeed);
            });
        }

        private int m_Width = 256;
        private int m_Height = 256;

        private HeightMapRandomizer m_Randomizer = new HeightMapRandomizer();
    }
}
