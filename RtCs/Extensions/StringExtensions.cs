namespace RtCs
{
    public static class StringExtensions
    {
        public static int IndexOfLineBreak(this string inValue)
            => inValue.IndexOfLineBreak(0, inValue.Length);

        public static int IndexOfLineBreak(this string inValue, int inStartIndex)
            => inValue.IndexOfLineBreak(inStartIndex, inValue.Length - inStartIndex);

        public static int IndexOfLineBreak(this string inValue, int inStartIndex, int inCount)
        {
            const char CR = '\r';
            const char LF = '\n';

            int crPos = inValue.IndexOf(CR, inStartIndex, inCount);
            if (crPos < 0) {
                return inValue.IndexOf(LF);
            }

            int lfPos = inValue.IndexOf(LF, inStartIndex, crPos - inStartIndex);
            if (lfPos < 0) {
                return crPos;
            }

            return lfPos;
        }

        public static int FindBetween(this string inValue, char inBegin, char inEnd, out string outString)
            => inValue.FindBetween(inBegin, inEnd, 0, inValue.Length, out outString);

        public static int FindBetween(this string inValue, char inBegin, char inEnd, int inStartIndex, out string outString)
            => inValue.FindBetween(inBegin, inEnd, inStartIndex, inValue.Length - inStartIndex, out outString);

        public static int FindBetween(this string inValue, char inBegin, char inEnd, int inStartIndex, int inCount, out string outString)
        {
            outString = "";

            int begin = inValue.IndexOf(inBegin, inStartIndex, inCount);
            if (begin < 0) {                
                return -1;
            }

            int end = inValue.IndexOf(inEnd, begin, (inStartIndex + inCount) - begin);
            if (end < 0) {
                return -1;
            }

            outString = inValue.Substring(begin, end - begin);
            return begin;
        }
    }
}
