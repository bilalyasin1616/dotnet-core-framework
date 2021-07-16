using System.Collections.Generic;
using System.Linq;

namespace Framework.Extensions
{
    public static class ExtensionService
    {
        /// <summary>
        /// Checks if a list of elements is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>True if empty</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;
            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }

        /// <summary>
        /// Checks if a list of elements is not null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>True if non empty</returns>
        public static bool NotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return false;
            var collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                return collection.Count > 0;
            }
            return enumerable.Any();
        }

        /// <summary>
        /// Adds updated item if the orignal item was null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">List<T> to add to</param>
        /// <param name="orignalItem">item to check from</param>
        /// <param name="updatedItem">item to add if orignal item is null</param>
        public static T ConditionalAdd<T>(this List<T> list, T orignalItem, T updatedItem)
        {
            if (orignalItem == null)
                list.Add(updatedItem);
            return updatedItem;
        }
    }
}
