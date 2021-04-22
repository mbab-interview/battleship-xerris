using System;
using System.Collections.Generic;

namespace battleship
{
    public static class StringExtensions
    {
        public static IEnumerable<string> SplitInParts(this string s, int length)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (length <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(length));

            for (var i = 0; i < s.Length; i += length)
                yield return s.Substring(i, Math.Min(length, s.Length - i));
        }
    }
}
