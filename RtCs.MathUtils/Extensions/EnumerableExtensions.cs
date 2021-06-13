using System;
using System.Collections.Generic;
using System.Linq;

namespace RtCs.MathUtils
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> inItems)
        {
            foreach (var items in inItems) {
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

        public static IEnumerable<T> Enumerate<T>(int inCount, Func<int, T> inInitializer)
            => inCount.Range().Select(i => inInitializer(i));

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
    }
}
