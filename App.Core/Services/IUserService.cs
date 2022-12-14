using App.Core.Dto;
using App.Core.Models;
namespace App.Core.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<CustomResponseDto<NoContentDto>> ChangeUserPassword(ChangeUserPasswordDto changeUserPasswordDto);
        Task<UserDto> PrepareRegisterModel(UserDto user);
        Task<CustomResponseDto<User>> ConfirmMail(ConfirmUserMailDto confirmUserMailDto);
        Task<User> ForgetMyPassword(ForgetPasswordDto forgetPasswordDto);
    }
}
