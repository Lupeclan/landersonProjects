using System.Collections.Generic;
using System.Linq;

namespace MangaRipper
{
    internal static class Extensions
    {
        internal static bool IsValid(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        internal static bool HasValues<T>(this IEnumerable<T> source)
        {
            return (source != null && source.Count() > 0);
        }
    }
}