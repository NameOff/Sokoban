using System.Collections.Generic;
using System.Linq;

namespace Sokoban.Infrastructure
{
    public static class EnumerableExtensions
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

        public static IEnumerable<T1> GetAll<T1, T2>(this IEnumerable<T2> source)
            where T1 : class
            where T2 : class
        {
            return source
                .Select(e => e as T1)
                .Where(e => e != null);
        }
    }
}