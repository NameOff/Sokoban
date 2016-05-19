using System.Collections.Generic;

namespace Sokoban.Infrastructure
{
    public static class EnumerableExtension
    {
        public static bool AllDifferent<T>(this IEnumerable<T> source)
        {
            var hashSet = new HashSet<T>();
            foreach (var e in source)
            {
                if (hashSet.Contains(e))
                    return false;
                hashSet.Add(e);
            }
            return true;
        }
    }
}