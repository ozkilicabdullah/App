using App.Core.Dto;
using App.Core.Models;
namespace App.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
    }
}
