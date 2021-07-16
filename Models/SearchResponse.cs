using System.Collections.Generic;

namespace Framework.Models
{
    public class SearchResponse<T>
    {
        public int TotalRecords { get; set; }
        public List<T> Records { get; set; }
    }
}
