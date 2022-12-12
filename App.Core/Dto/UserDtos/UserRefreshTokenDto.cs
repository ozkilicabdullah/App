namespace App.Core.Dto
{
    public class UserRefreshTokenDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
