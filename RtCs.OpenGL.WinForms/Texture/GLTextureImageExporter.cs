using RtCs.MathUtils;
using RtCs.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.OpenGL.WinForms
{
    public class GLTextureImageExporter
    {
        public void ExportToFile(IGLColorTexture2D inSource, string inFilepath)
        {
            ImageFormat format;
            string extension = Path.GetExtension(inFilepath);
            switch (extension.ToLower()) {
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
                case ".gif":
                    format = ImageFormat.Gif;
                    break;
                case ".ico":
                    format = ImageFormat.Icon;
                    break;
                case ".jpg":
                case ".jpeg":
                    format = ImageFormat.Jpeg;
                    break;
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".tif":
                case ".tiff":
                    format = ImageFormat.Tiff;
                    break;
                default:
                    throw new ArgumentException($"Not support format for extension \"{extension}\".");
            }
            ExportToFile(inSource, inFilepath, format);
        }

        public void ExportToFile(IGLColorTexture2D inSource, string inFilepath, ImageFormat inFormat)
        {
            using (FileStream stream = new FileStream(inFilepath, FileMode.OpenOrCreate, FileAccess.Write)) {
                ExportToStream(inSource, stream, inFormat);
            }
            return;
        }

        public void ExportToStream(IGLColorTexture2D inSource, Stream inDestination, ImageFormat inFormat)
        {
            using (Bitmap image = ExportToImage(inSource)) {
                image.Save(inDestination, inFormat);
            }
            return;
        }

        public Bitmap ExportToImage(IGLColorTexture2D inSource)
        {
            Bitmap result = new Bitmap(inSource.Width, inSource.Height, PixelFormat.Format32bppArgb);

            var bmpData = result.LockBits(result.GetFullRect(), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            try {
                byte[] bytes = RgbasToBytes(inSource.Pixels);
                Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);

            } finally {
                result.UnlockBits(bmpData);
            }
            return result;
        }

        private byte[] RgbasToBytes(ColorRGBA[] inItems)
        {
            byte[] result = new byte[inItems.Length * 4];
            for (int i = 0; i < inItems.Length; ++i) {
                ColorRGBA item = inItems[i];
                result[(i * 4) + 2] = item.R;
                result[(i * 4) + 1] = item.G;
                result[(i * 4) + 0] = item.B;
                result[(i * 4) + 3] = item.A;
            }
            return result;
        }
    }
}
