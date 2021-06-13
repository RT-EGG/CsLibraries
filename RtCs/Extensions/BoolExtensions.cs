using System;

namespace RtCs
{
    public static class BoolExtensions
    {
        public static void When(this bool inCondition, Action inAction)
        {
            if (inCondition) {
                inAction();
            }
            return;
        }
    }
}
