using Framework.Exceptions;
using Framework.Models;

namespace Framework.Extensions
{
    public static class Search
    {
        public static void ValidateSearch<E>(this SearchRequest searchRequest)
        {
            var sortBy = searchRequest.SortBy;
            if (sortBy != null && sortBy != "" && !typeof(E).CheckColumnExist(sortBy))
                throw new BadRequestException("Invalid sort by column");
            var sortOrder = searchRequest.SortOrder;
            if (!string.IsNullOrEmpty(sortOrder) && (sortOrder != "asc" || sortBy != "desc"))
                throw new BadRequestException("Invalid sort order, can only use 'asc' and desc as values");
        }
    }
}
