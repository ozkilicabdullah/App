using App.Core.Dto;
using App.Core.Models;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using AutoMapper;

namespace App.Service.Services
{
    public class UserRefreshTokenServiceImp : GenericServiceImp<UserRefreshToken>, IUserRefreshTokenService
    {
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

        public UserRefreshTokenServiceImp(IGenericRepository<UserRefreshToken> repository, IUnitOfWork unitOfWork, IUserRefreshTokenRepository userRefreshTokenRepository) : base(repository, unitOfWork)
        {
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<UserRefreshToken> GetByRefreshToken(string refreshToken)
        {
            return await _userRefreshTokenRepository.GetByRefreshToken(refreshToken);

        }

        public async Task<UserRefreshToken> GetByUserId(int id)
        {
            return await _userRefreshTokenRepository.GetByUserId(id);
        }
    }
}
