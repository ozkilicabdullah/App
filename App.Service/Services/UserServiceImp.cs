using App.Core.Dto;
using App.Core.Models;
using App.Core.Repositories;
using App.Core.Services;
using App.Core.UnitOfWorks;
using App.Service.Services.SenderService.Email;
using App.Shared.Helpers;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace App.Service.Services
{
    public class UserServiceImp : GenericServiceImp<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly EmailSenderService _emailSenderService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailSettingService _emailSettingService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserServiceImp(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IEmailTemplateService emailTemplateService, IEmailSettingService emailSettingService, IMapper mapper, IConfiguration configuration) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _emailTemplateService = emailTemplateService;
            _emailSettingService = emailSettingService;
            _emailSenderService = new EmailSenderService(_emailSettingService, _emailTemplateService);
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<CustomResponseDto<NoContentDto>> ChangeUserPassword(ChangeUserPasswordDto changeUserPasswordDto)
        {
            var user = await _userRepository.GetUserByEmail(changeUserPasswordDto.Email);
            if (user == null)
                return CustomResponseDto<NoContentDto>.Fail(400, "User not found!");

            user.Password = changeUserPasswordDto.Password;
            _userRepository.Update(user);
            return CustomResponseDto<NoContentDto>.Success(200);
        }
        public async Task<CustomResponseDto<User>> ConfirmMail(ConfirmUserMailDto confirmUserMailDto)
        {
            var user = await _userRepository.ConfirmMailForUser(confirmUserMailDto);
            if (user == null)
                return CustomResponseDto<User>.Fail(400, "User not found!");
            return CustomResponseDto<User>.Success(200, user);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }
        public async Task<UserDto> PrepareRegisterModel(UserDto user)
        {

            user.Password = SecureOperations.MD5Hash(user.Password);
            user.EmailConfirmationSecretKey = SecureOperations.MD5Hash(string.Concat(user.Name, user.Email));
            user.IsEmailConfirmed = false;
            user.Role = "user";
            await SendConfirmationMail(_mapper.Map<User>(user));
            return user;
        }
        private async Task<bool> SendConfirmationMail(User user)
        {
            string confirmationMailUIRoute = _configuration.GetSection("ConfirmationMailUIRoute").Value;
            ReplaceItem replaceItem = new() { Key = "MailConfirmationValidationSecretKey", Value = string.Concat(confirmationMailUIRoute, user.EmailConfirmationSecretKey, "&Email=", user.Email) };
            EmailSendRequestDto emailSendRequestDto = new EmailSendRequestDto()
            {
                ToName = user.Name,
                ToEmail = user.Email,
                SendingContentType = SendingContentType.EmailConfirmation,
                Subject = "Üyelik Onay Maili"
            };
            emailSendRequestDto.AddReplacePair(replaceItem.Key, replaceItem.Value);
            var response = await _emailSenderService.Send(emailSendRequestDto);
            if (!response.IsSuccess)
                return false; // The process for resending must be improved.
            return true;
        }
        public async Task<User> ForgetMyPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userRepository.GetUserByEmail(forgetPasswordDto.Email);
            if (user == null)
                return null;
            string forgetMyPasswordMailUIRoute = _configuration.GetSection("ForgetMyPasswordMailUIRoute").Value;
            user.ResetPasswordSecretKey = SecureOperations.MD5Hash(String.Concat(user.Password, user.Email, (DateTime.Now.Minute * DateTime.Now.Second).ToString()));
            ReplaceItem replaceItem = new() { Key = "ResetPasswordSecretKey", Value = string.Concat(forgetMyPasswordMailUIRoute, user.ResetPasswordSecretKey, "&Email=", user.Email) };
            EmailSendRequestDto emailSendRequestDto = new EmailSendRequestDto()
            {
                ToName = user.Name,
                ToEmail = user.Email,
                SendingContentType = SendingContentType.ResetPassword,
                Subject = "Şifremi Unuttum!"
            };
            emailSendRequestDto.AddReplacePair(replaceItem.Key, replaceItem.Value);
            await _emailSenderService.Send(emailSendRequestDto);
            return user;
        }
        /// <summary>
        /// How many new users were successfully registered over a period of time (e.g. 1 day)
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<int> SuccessFulUserRegisteration(DateTime BeginDate, DateTime EndDate)
        {
            var record = await _userRepository.SuccessFulUserRegisteration(BeginDate, EndDate);
            return record.SuccessFullRegisterationUserCount;
        }
        /// <summary>
        /// How many users who were sent a verification code but did not register after 1 day
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<int> UnApprovedUsers(DateTime BeginDate, DateTime EndDate)
        {
            var record = await _userRepository.UnApprovedUsers(BeginDate, EndDate);
            return record.UnApprovedUsersCount;
        }
        /// <summary>
        /// How long it takes to complete login (in seconds, average for a given day)
        /// </summary>
        /// <param name="BeginDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<int> AvarageRegisterationComplationTime(DateTime BeginDate, DateTime EndDate)
        {
            var record = await _userRepository.AvarageRegisterationComplationTime(BeginDate, EndDate);
            return record.AvarageRegisterationComplationTime;
        }
    }
}
