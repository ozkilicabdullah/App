using App.Core.Dto;
using App.Core.Models;
using App.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Repositories
{
    public class UserRepositoryImp : GenericRepositoryImp<User>, IUserRepository
    {
        public UserRepositoryImp(AppDbContext context) : base(context)
        {
        }

        public async Task<User> ConfirmMailForUser(ConfirmUserMailDto confirmUserMailDto)
        {
            return await (from u in _context.Users where u.Email == confirmUserMailDto.Email && u.EmailConfirmationSecretKey == confirmUserMailDto.SecretKey select u).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await (from u in _context.Users where u.Email == email select u).FirstOrDefaultAsync();
        }

        public async Task<AvarageRegisterationComplationTimeDto> AvarageRegisterationComplationTime(DateTime BeginDate, DateTime EndDate)
        {
            List<AvarageCompareUserConfirmationTimeDto> recordAvarageTimes = new();
            recordAvarageTimes = await (from u in _context.Users
                                        where u.IsEmailConfirmed == true
                                          && u.MailConfirmedDate != null
                                          && u.MailConfirmedDate >= BeginDate
                                          && u.MailConfirmedDate <= EndDate
                                        select new AvarageCompareUserConfirmationTimeDto
                                        {
                                            CreatedDate = u.CreatedOn,
                                            ConfirmDate = u.MailConfirmedDate.GetValueOrDefault()
                                        }).ToListAsync();
            AvarageRegisterationComplationTimeDto avarageRegisterationComplationTimeDto = new();
            if (recordAvarageTimes.Count > 0)
            {
                double totalMinutes = 0;
                foreach (var item in recordAvarageTimes)
                {
                    TimeSpan ts = item.ConfirmDate - item.CreatedDate;
                    totalMinutes += ts.TotalMinutes;
                }
                totalMinutes = totalMinutes / recordAvarageTimes.Count;
                avarageRegisterationComplationTimeDto.AvarageRegisterationComplationTime = Convert.ToInt32(totalMinutes);
            }
            return avarageRegisterationComplationTimeDto;
        }

        public async Task<SuccessFulUserRegisterationDto> SuccessFulUserRegisteration(DateTime BeginDate, DateTime EndDate)
        {
            int recordCount = await (from u in _context.Users
                                     where u.IsEmailConfirmed == true
                                     && u.MailConfirmedDate >= BeginDate
                                     && u.MailConfirmedDate <= EndDate
                                     select u.Id).CountAsync();
            SuccessFulUserRegisterationDto successFulUserRegisterationDto = new() { SuccessFullRegisterationUserCount = recordCount };
            return successFulUserRegisterationDto;
        }

        public async Task<UnApprovedUsersDto> UnApprovedUsers(DateTime BeginDate, DateTime EndDate)
        {
            int recordCount = await (from u in _context.Users
                                     where u.IsEmailConfirmed == false
                                     && u.CreatedOn >= BeginDate
                                     && u.CreatedOn <= EndDate
                                     select u.Id).CountAsync();
            UnApprovedUsersDto unApprovedUsersDto = new() { UnApprovedUsersCount = recordCount };
            return unApprovedUsersDto;
        }
    }
}
