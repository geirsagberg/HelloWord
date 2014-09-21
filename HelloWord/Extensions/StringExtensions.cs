using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace HelloWord.Extensions
{
    public static class StringExtensions
    {
        [StringFormatMethod("str")]
        public static string FormatWith(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static string ToJoinedString<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable.Select(e => e.ToString()));
        }
    }
}