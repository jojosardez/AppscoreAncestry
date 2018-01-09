using System;

namespace AppscoreAncestry.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string value, StringComparison compare)
        {
            return source.IndexOf(value, compare) >= 0;
        }
    }
}
