using App.Core.Models;
using System.Text.Json.Serialization;

namespace App.Core.Dto
{
    public class BaseDtoModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedOn { get; set; }
        [JsonIgnore]
        public int? CreatedBy { get; set; }
        [JsonIgnore]
        public int? ModifiedBy { get; set; }
        public Status Status { get; set; }
    }
}
