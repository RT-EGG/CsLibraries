namespace RtCs
{
    public struct ColorRGBA
    {
        public ColorRGBA(byte inR, byte inG, byte inB, byte inA = 255)
        {
            R = inR;
            G = inG;
            B = inB;
            A = inA;
            return;
        }

        public ColorRGBA(ColorRGB inRGB, byte inA = 255)
            : this(inRGB.R, inRGB.G, inRGB.B, inA)
        { }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public ColorRGB RGB
        {
            get => new ColorRGB(R, G, B);
            set {
                R = value.R;
                G = value.G;
                B = value.B;
                return;
            }
        }

        public static implicit operator ColorRGB(ColorRGBA inSource) => new ColorRGB(inSource.R, inSource.G, inSource.B);
    }
}
