using App.API.Filters;
using App.Core.Dto;
using App.Core.Models;
using App.Core.Services;
using App.Shared.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        #region Ctor
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        #endregion

        #region Get Methods
        [Authorize]
        [HttpGet("UserInfo")]
        public async Task<IActionResult> GetCurrentUser()
        {
            string email = HttpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(email))
                return CreateActionResult(CustomResponseDto<UserDto>.Fail(400, "User not found!"));

            var user = await _userService.GetUserByEmail(email);
            if (user == null)
                return CreateActionResult(CustomResponseDto<UserDto>.Fail(400, "User not found!"));

            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }
        #endregion

        #region Post Methods
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserCreateDto userCreateDto)
        {
            var existUser = await _userService.GetUserByEmail(userCreateDto.Email);
            if (existUser != null)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "There is a user for this email. If you forgot your password, use the reset password usage step!"));

            // Prepera user model 
            var preperadSecurityUserDto = await _userService.PrepareRegisterModel(_mapper.Map<UserDto>(userCreateDto));
            await _userService.AddAsync(_mapper.Map<User>(preperadSecurityUserDto));

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmUserMailDto confirmUserMailDto)
        {
            var response = await _userService.ConfirmMail(confirmUserMailDto);
            if (!response.IsSuccess)
                return CreateActionResult(CustomResponseDto<UserDto>.Fail(400, response.Errors));

            // update user
            var user = response.Data;
            user.IsEmailConfirmed = true;
            user.MailConfirmedDate = DateTime.Now;
            await _userService.UpdateAsync(user);
            var userDto = _mapper.Map<UserDto>(user);
            return CreateActionResult(CustomResponseDto<UserDto>.Success(200, userDto));
        }

        [HttpPost("ForgetMyPassword")]
        public async Task<IActionResult> ForgetMyPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userService.ForgetMyPassword(forgetPasswordDto);
            await _userService.UpdateAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        } 

        [HttpPost("ChangePasswordForForgotten")]
        public async Task<IActionResult> ChangePasswordForForgotten(ChangePasswordForForgottenDto changePasswordForForgottenDto)
        {
            var user = await _userService.GetUserByEmail(changePasswordForForgottenDto.Email);
            if (user == null || user.ResetPasswordSecretKey != changePasswordForForgottenDto.SecretKey)
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "User not found!"));

            user.Password = SecureOperations.MD5Hash(changePasswordForForgottenDto.Password);
            user.ForgetPaswordSecretKey = null;
            await _userService.UpdateAsync(user);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        #endregion
    }
}

