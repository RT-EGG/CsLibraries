using RtCs.MathUtils;
using RtCs.WinForms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace RtCs.OpenGL.WinForms
{
    public class GLTextureImageImporter
    {
        public void ImportFromFile(string inFilepath, GLColorTexture2D inDestination)
            => ImportFromImage(new Bitmap(inFilepath), inDestination);

        public void ImportFromStream(Stream inStream, GLColorTexture2D inDestination)
            => ImportFromImage(new Bitmap(inStream), inDestination);

        public void ImportFromImage(Bitmap inImage, GLColorTexture2D inDestination)
        {
            ColorRGBA[] pixels = null;

            var bmpData = inImage.LockBits(inImage.GetFullRect(), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            try {
                byte[] bytes = new byte[inImage.Width * inImage.Height * 4];
                Marshal.Copy(bmpData.Scan0, bytes, 0, bytes.Length);
                pixels = BytesToRgbas(bytes);

            } finally {
                inImage.UnlockBits(bmpData);
            }

            inDestination.ResizeBuffer(inImage.Width, inImage.Height);
            inDestination.Pixels = pixels;
            inDestination.Apply();
            return;
        }

        private ColorRGBA[] BytesToRgbas(byte[] inItems)
        {
            ColorRGBA[] result = new ColorRGBA[inItems.Length / 4];
            for (int i = 0; i < result.Length; ++i) {
                result[i] = new ColorRGBA(
                        inItems[(i * 4) + 2],
                        inItems[(i * 4) + 1],
                        inItems[(i * 4) + 0],
                        inItems[(i * 4) + 3]
                    );
            }
            return result;
        }
    }
}
