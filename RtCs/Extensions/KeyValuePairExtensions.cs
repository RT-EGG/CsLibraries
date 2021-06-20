using System.Collections.Generic;

namespace RtCs
{
    public static class KeyValuePairExtensions
    {
        public static void Deconstruct<Key, Value>(this KeyValuePair<Key, Value> inPair, out Key outKey, out Value outValue)
            => (outKey, outValue) = (inPair.Key, inPair.Value);            
    }
}
