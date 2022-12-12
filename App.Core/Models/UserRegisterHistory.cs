namespace App.Core.Models
{
    public class UserRegisterHistory : DbBaseModel
    {
        public int UserId { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public User User { get; set; } // Navigate property

    }
}
