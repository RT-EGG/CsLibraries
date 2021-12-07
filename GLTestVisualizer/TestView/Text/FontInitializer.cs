using System;
using System.Collections.Generic;
using System.Drawing;

namespace GLTestVisualizer.TestView.Text
{
    public struct FontInitializer : IEquatable<FontInitializer>
    {
        public FontInitializer(Font inSource)
            : this (inSource.Name, inSource.SizeInPoints * PointToPixels, inSource.Style)
        { }

        public FontInitializer(string inFontName, float inFontSizeInPixels, FontStyle inFontStyle)
        {
            FontName = inFontName;
            FontSizeInPixels = inFontSizeInPixels;
            FontStyle = inFontStyle;
        }

        public readonly string FontName;
        public readonly float FontSizeInPixels;
        public readonly FontStyle FontStyle;

        public static float PointToPixels
        { get; set; } = 96.0f / 72.0f;

        public override bool Equals(object obj)
            => obj is FontInitializer initializer && Equals(initializer);

        public bool Equals(FontInitializer other)
            => FontName == other.FontName &&
               FontSizeInPixels == other.FontSizeInPixels &&
               FontStyle == other.FontStyle;

        public override int GetHashCode()
        {
            int hashCode = -822346801;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FontName);
            hashCode = hashCode * -1521134295 + FontSizeInPixels.GetHashCode();
            hashCode = hashCode * -1521134295 + FontStyle.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(FontInitializer left, FontInitializer right)
            => left.Equals(right);
        public static bool operator !=(FontInitializer left, FontInitializer right)
            => !(left == right);
    }
}
