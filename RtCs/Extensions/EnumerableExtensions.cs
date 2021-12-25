using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtCs
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> inItems)
        {
            foreach (var items in inItems) {
                if (items == null) {
                    continue;
                }

                foreach (var item in items) {
                    yield return item;
                }
            }
            yield break;
        }

        public static void ForEach<T>(this IEnumerable<T> inItems, Action<T> inAction)
        {
            foreach (var item in inItems) {
                inAction(item);
            }
            return;
        }

        public static IEnumerable<(int, T)> Enumerate<T>(this IEnumerable<T> inItems)
        {
            int i = 0;
            foreach (var item in inItems) {
                yield return (i++, item);
            }
            yield break;
        }

        public static IEnumerable<T> AsEnumerable<T>(params T[] inValues)
            => inValues;

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> inFirst, params T[] inSecond)
            => inFirst.Concat((IEnumerable<T>)inSecond);

        public static IEnumerable<int> Range(this int inCount)
            => Enumerable.Range(0, inCount);

        public static IEnumerable<T> Enumerate<T>(this int inCount, Func<int, T> inInitializer)
            => inCount.Range().Select(i => inInitializer(i));

        public static bool TryGetFirst<T>(this IEnumerable<T> inItems, out T outValue, Predicate<T> inComparer)
        {
            outValue = default;
            foreach (var item in inItems) {
                if (inComparer(item)) {
                    outValue = item;
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<T> ElementsAt<T>(this IReadOnlyList<T> inItems, IEnumerable<int> inIndices)
        {
            foreach (var index in inIndices) {
                yield return inItems[index];
            }
            yield break;
        }

        public static IEnumerable<T> ElementsAt<T>(this IReadOnlyList<T> inItems, ICollection<int> inIndices)
        {
            T[] result = new T[inIndices.Count];
            int i = 0;
            foreach (var index in inIndices) {
                result[i++] = inItems[index];
            }
            return result;
        }

        public static bool IsEmpty(this IEnumerable inItems)
        {
            foreach (var _ in inItems) {
                return false;
            }
            return true;
        }

        public static bool IsNullOrEmpty(this IEnumerable inItems)
            => (inItems == null) || inItems.IsEmpty();

        public static void Add<T>(this IList<T> inItems, params T[] inNewItems)
        {
            foreach (var newItem in inNewItems) {
                inItems.Add(newItem);
            }
            return;
        }

        public static void DisposeItems<T>(this IEnumerable<T> inItems) where T : IDisposable
            => inItems.ForEach(item => item.Dispose());

        public static int MinIndex<T, U>(this IReadOnlyList<T> inItems, Func<T, U> inSelector) where U : IComparable<U>
        {
            if (inItems.IsEmpty()) {
                return -1;
            }

            int result = 0;
            U minValue = inSelector(inItems[0]);

            foreach (var (i, item) in inItems.Enumerate()) {
                U value = inSelector(item);
                if (minValue.CompareTo(value) > 0) {
                    result = i;
                    minValue = value;
                }
            }
            return result;
        }

        public static int MinIndex<T>(this IReadOnlyList<T> inItems) where T : IComparable<T>
            => inItems.MinIndex(t => t);

        public static int MaxIndex<T, U>(this IReadOnlyList<T> inItems, Func<T, U> inSelector) where U : IComparable<U>
        {
            if (inItems.IsEmpty()) {
                return -1;
            }

            int result = 0;
            U minValue = inSelector(inItems[0]);

            foreach (var (i, item) in inItems.Enumerate()) {
                U value = inSelector(item);
                if (minValue.CompareTo(value) < 0) {
                    result = i;
                    minValue = value;
                }
            }
            return result;
        }

        public static int MaxIndex<T>(this IReadOnlyList<T> inItems) where T : IComparable<T>
            => inItems.MaxIndex(t => t);
    }
}
