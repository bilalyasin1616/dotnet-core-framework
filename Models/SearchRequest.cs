using System.Collections.Generic;

namespace Framework.Models
{
    public class SearchRequest
    {
        public List<FilterTermModel> Filters { get; set; }
        public string Range { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public string Includes { get; set; }
    }

    public class FilterTermModel
    {
        public List<string> Columns { get; set; }
        public string Term { get; set; }
    }
}
