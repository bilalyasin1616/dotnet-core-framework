using System.Collections.Generic;

namespace Framework.Models
{
    public class SearchRequest
    {
        const string DefaultOrderBy = "Id";
        public const string DefaultSortOrder = "desc";
        const int DefaultAfter = 0;
        const int DefaultLimit = 10;

        public Dictionary<string, string> Filters { get; set; }
        public string OrderBy { get; set; } = DefaultOrderBy;
        public string SortOrder { get; set; } = DefaultSortOrder;
        public int After { get; set; } = DefaultAfter;
        public int Limit { get; set; } = DefaultLimit;
    }
}
