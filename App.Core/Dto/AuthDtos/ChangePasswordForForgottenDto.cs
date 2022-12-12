namespace App.Core.Dto
{
    public class ChangePasswordForForgottenDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
    }
}
