using System.Collections.Generic;
using System.Linq;

namespace AudioTaggerCore
{
    public static class ExtensionMethods
    {
        public static bool IsValid<T>(this T[] source)
        {
            return (source != null && source.Length > 0);
        }

        public static bool IsValid<T>(this IEnumerable<T> source)
        {
            return (source != null && source.Count() > 0);
        }
    }
}