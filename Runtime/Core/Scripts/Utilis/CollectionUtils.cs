using System;
using System.Collections.Generic;

namespace LonecraftGames.Toolkit.Core.Utilis
{
    public static class CollectionUtils
    {
        /// <summary>
        ///  Checks if a collection is null or has no elements.
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
            => collection == null || collection.Count == 0;

        /// <summary>
        ///  Executes the given action for each element in the source collection.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null || action == null) return;
            foreach (var item in source) action(item);
        }
    }
}