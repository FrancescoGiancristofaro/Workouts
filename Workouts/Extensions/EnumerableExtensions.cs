using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool SafeAny<T>(this IEnumerable<T> collection) 
        {
            return collection is not null && collection.Any();
        }

        public static bool SafeAny<T>(this IEnumerable<T> collection,Func<T,bool> predicate)
        {
            return collection is not null && collection.Any(predicate);
        }
    }
}
