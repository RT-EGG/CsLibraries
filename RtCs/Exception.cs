using System;

namespace RtCs
{
    public class InvalidEnumValueException<T> : Exception where T : Enum
    {
        public InvalidEnumValueException(T inValue)
            : base($"{(int)(object)inValue} is invalid value of {typeof(T).Name}.")
        { }
    }
}
