using System;
using System.Collections.Generic;

namespace RtCs
{
    public struct Container3<T> : IContainer<T>, IEquatable<Container3<T>>
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

        public void Set(T inItem0, T inItem1, T inItem2)
        {
            Item0 = inItem0;
            Item1 = inItem1;
            Item2 = inItem2;
            return;
        }

        public override string ToString()
            => $"[{Item0}, {Item1}, {Item2}]";

        public override bool Equals(object obj)
            => obj is Container3<T> container && Equals(container);

        public bool Equals(Container3<T> other)
        {
            var comparer = EqualityComparer<T>.Default;
            return comparer.Equals(Item0, other.Item0)
                && comparer.Equals(Item1, other.Item1)
                && comparer.Equals(Item2, other.Item2);
        }

        public override int GetHashCode()
        {
            var comparer = EqualityComparer<T>.Default;

            int hashCode = -1268265253;
            hashCode = hashCode * -1521134295 + comparer.GetHashCode(Item0);
            hashCode = hashCode * -1521134295 + comparer.GetHashCode(Item1);
            hashCode = hashCode * -1521134295 + comparer.GetHashCode(Item2);
            return hashCode;
        }

        public static bool operator ==(Container3<T> left, Container3<T> right)
            => left.Equals(right);

        public static bool operator !=(Container3<T> left, Container3<T> right)
            => !(left == right);
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
