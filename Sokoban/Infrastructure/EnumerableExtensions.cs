using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
