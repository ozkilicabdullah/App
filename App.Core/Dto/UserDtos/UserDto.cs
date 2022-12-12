
using System.Text.Json.Serialization;

namespace App.Core.Dto
{
    public class UserDto : BaseDtoModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [JsonIgnore]
        public string EmailConfirmationSecretKey { get; set; }
        public string Role { get; set; }
    }
}
