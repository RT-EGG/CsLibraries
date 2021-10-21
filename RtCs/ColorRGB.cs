namespace RtCs
{
    public struct ColorRGB
    {
        public ColorRGB(byte inR, byte inG, byte inB)
        {
            R = inR;
            G = inG;
            B = inB;
            return;
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}
