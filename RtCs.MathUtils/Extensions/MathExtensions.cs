using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public static class MathExtensions
    {
        public static Vector3 Average(this IEnumerable<Vector3> inItems)
        {
            Vector3 result = new Vector3();
            int count = 0;
            foreach (var item in inItems) {
                result += item;
                ++count;
            }
            if (count > 0) {
                return result / count;
            }
            return result;
        }
    }
}
