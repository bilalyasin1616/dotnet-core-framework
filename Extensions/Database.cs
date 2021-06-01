using System.Linq;

namespace Framework.Extensions
{
    public static class Database
    {
        public static string DefaultSortOrder { get; } = "desc";
        public static string DefaultSortColumn { get; } = "Id";

        public static IQueryable<T> SortBy<T>(this IQueryable<T> dbSet, string sortBy, string sortOrder = "desc", string defaultSortColumn = "Id") where T : class
        {
            if (sortOrder == null)
                sortOrder = "desc";
            if (sortBy == null)
                sortBy = defaultSortColumn;
            return sortOrder == "desc" ?
                dbSet.OrderByDescending(o => o.GetByColumn(sortBy, defaultSortColumn))
                : dbSet.OrderBy(o => o.GetByColumn(sortBy, defaultSortColumn));
        }

        private static object GetByColumn(this object obj, string sortBy, string defaultSortColumn)
        {
            return string.IsNullOrEmpty(sortBy) ? 
                obj.GetType().GetProperty(sortBy).GetValue(obj) :
                obj.GetType().GetProperty(defaultSortColumn).GetValue(obj);
        }

    }
}
