using System;

namespace RtCs
{
    public struct Container3<T> : IContainer<T>
    {
        public Container3(T inItem0, T inItem1, T inItem2)
        {
            Item0 = inItem0;
            Item1 = inItem1;
            Item2 = inItem2;
        }

        public T this[int inIndex]
        {
            get {
                switch (inIndex) {
                    case 0: return Item0;
                    case 1: return Item1;
                    case 2: return Item2;
                }
                throw new ArgumentOutOfRangeException();
            }
            set {
                switch (inIndex) {
                    case 0: Item0 = value; break;
                    case 1: Item1 = value; break;
                    case 2: Item2 = value; break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public T Item0
        { get; set; }
        public T Item1
        { get; set; }
        public T Item2
        { get; set; }

        public int Count => 3;
    }

    public static class Container3Extensions
    {
        public static void Deconstruct<T>(this Container3<T> inValue, out T outItem0, out T outItem1, out T outItem2)
        {
            outItem0 = inValue[0];
            outItem1 = inValue[1];
            outItem2 = inValue[2];
        }
    }
}
