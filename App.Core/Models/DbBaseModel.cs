
namespace App.Core.Models
{
    public class DbBaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public Status Status { get; set; }
    }
}
