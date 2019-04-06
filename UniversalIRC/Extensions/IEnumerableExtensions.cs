using System;
using System.Linq;
using System.Collections.Generic;

namespace UniversalIRC.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TResult> WhereAllAs<TResult, TSource>(this IEnumerable<TSource> source)
            where TResult : class
        {
            return source.Where(s => s is TResult).Select(i => i as TResult);
        }
    }
}
