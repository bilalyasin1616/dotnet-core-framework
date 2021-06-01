using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Framework.Models
{
    [Serializable]
    abstract public class BaseAuditModel
    {
        public virtual int Id { get; set; }

        [JsonIgnore]
        public bool IsActive { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? DateCreated { get; set; }

        [StringLength(150)]
        public string CreatedByName { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        public DateTime? DateLastUpdated { get; set; }

        [StringLength(150)]
        public string LastUpdateByName { get; set; }

        public int? LastUpdatedBy { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
