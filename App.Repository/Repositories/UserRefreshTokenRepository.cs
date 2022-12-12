using App.Core.Models;
using App.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Repositories
{
    public class UserRefreshTokenRepository : GenericRepositoryImp<UserRefreshToken>, IUserRefreshTokenRepository
    {
        public UserRefreshTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<UserRefreshToken> GetByRefreshToken(string refreshToken)
        {
            var query = (from rt in _context.UserRefreshTokens where rt.RefreshToken == refreshToken select rt);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<UserRefreshToken> GetByUserId(int id)
        {
            var query = (from rt in _context.UserRefreshTokens where rt.UserId == id select rt);
            return await query.FirstOrDefaultAsync();
        }
    }
}
