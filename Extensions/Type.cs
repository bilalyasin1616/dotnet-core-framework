using System;
using System.Linq;

namespace Framework.Extensions
{
    public static class TypeExtensions
    {
        public static bool CheckColumnExist(this Type source, string column)
        {
            var sourceProps = source.GetProperties().Where(x => x.CanRead).ToList();
            return sourceProps.Any(x => x.Name.ToLower() == column.ToLower());
        }
    }
}