using System.ComponentModel.DataAnnotations;

namespace Framework.Models
{
    public class SearchResourceBase
    {
        [Required]
        public int After { get; set; }
        public int Limit { get; set; }
        [Required]
        public string SortBy { get; set; }
        [Required]
        public string SortOrder { get; set; }
        public string Includes { get; set; }
    }
}
