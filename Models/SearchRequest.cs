namespace Framework.Models
{
    public class SearchRequest<FiltersType>
    {
        static string DefaultOrderBy = "Id";
        public static string DefaultSortOrder = "desc";
        const int DefaultAfter = 0;
        const int DefaultLimit = 10;

        public FiltersType Filters { get; set; }
        public string OrderBy { get; set; } = DefaultOrderBy;
        public string SortOrder { get; set; } = DefaultSortOrder;
        public int After { get; set; } = DefaultAfter;
        public int Limit { get; set; } = DefaultLimit;
    }
}
