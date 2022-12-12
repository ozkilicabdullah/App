namespace App.Core.Models
{
    public class User : DbBaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime? MailConfirmedDate { get; set; }
        public string? ForgetPaswordSecretKey { get; set; }
        public string? EmailConfirmationSecretKey { get; set; }
        public string? ResetPasswordSecretKey { get; set; }
        public ICollection<UserRegisterHistory> UserRegisterHistories { get; set; }
    }
}
