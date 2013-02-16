using System;

namespace Chat
{
    public static class Utility
    {
        public static T ConvertTo<T>(this string source)
        {
            if (!typeof (T).IsEnum)
                throw new NotSupportedException("T must be an enum");
            return (T) Enum.Parse(typeof (T), source);
        }
    }
}