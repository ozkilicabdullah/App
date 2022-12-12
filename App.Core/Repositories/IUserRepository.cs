using App.Core.Dto;
using App.Core.Models;
namespace App.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> ConfirmMailForUser(ConfirmUserMailDto confirmUserMailDto);
        Task<SuccessFulUserRegisterationDto> SuccessFulUserRegisteration(DateTime BeginDate, DateTime EndDate);
        Task<UnApprovedUsersDto> UnApprovedUsers(DateTime BeginDate, DateTime EndDate);
        Task<AvarageRegisterationComplationTimeDto> AvarageRegisterationComplationTime(DateTime BeginDate, DateTime EndDate);
    }
}
