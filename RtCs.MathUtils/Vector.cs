using System.Collections.Generic;

namespace RtCs.MathUtils
{
    public interface IVector : IReadOnlyList<float>, IEnumerable<float>
    {
        new float this[int inIndex] { get; set; }
    }
}
