using App.Core.Dto;
using App.Core.Models;

namespace App.Core.Services
{
    public interface IUserRefreshTokenService : IGenericService<UserRefreshToken>
    {
        Task<UserRefreshToken> GetByUserId(int id);
        Task<UserRefreshToken> GetByRefreshToken(string refreshToken);
    }
}
