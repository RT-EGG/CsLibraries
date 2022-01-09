using RtCs.MathUtils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtCs.WinForms
{
    public static class ColorExtensions
    {
        public static Color Interpolate(this Color inColor1, Color inColor2)
        {
            float r = (((inColor1.R / 255.0f) + (inColor2.R / 255.0f)) * 0.5f).Clamp(0.0f, 1.0f);
            float g = (((inColor1.G / 255.0f) + (inColor2.G / 255.0f)) * 0.5f).Clamp(0.0f, 1.0f);
            float b = (((inColor1.B / 255.0f) + (inColor2.B / 255.0f)) * 0.5f).Clamp(0.0f, 1.0f);
            float a = (((inColor1.A / 255.0f) + (inColor2.A / 255.0f)) * 0.5f).Clamp(0.0f, 1.0f);
            return Color.FromArgb(
                    (byte)(255 * a),
                    (byte)(255 * r),
                    (byte)(255 * g),
                    (byte)(255 * b)
                );
        }
    }
}
