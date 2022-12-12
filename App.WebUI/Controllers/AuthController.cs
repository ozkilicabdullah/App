using App.Core.Dto;
using App.WebUI.Models;
using App.WebUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiService _authApiService;
        private readonly UserApiService _userApiService;
        public AuthController(AuthApiService authApiService, UserApiService userApiService)
        {
            _authApiService = authApiService;
            _userApiService = userApiService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            OperationResult operationResult = new();
            if (loginViewModel == null)
                return Json(operationResult);

            var tokenResponse = await _authApiService.Login(loginViewModel);
            operationResult.Success = tokenResponse.IsSuccess;
            if (tokenResponse.IsSuccess)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,loginViewModel.Email),
                    };

                var useridentity = new ClaimsIdentity(claims, "AVS");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                operationResult.IsRedirect = true;
                operationResult.Message = "/Home/Index";
            }
            else
                operationResult.Errors = tokenResponse.Errors;

            return Json(operationResult);

        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            OperationResult operationResult = new();
            if (registerViewModel == null)
                return Json(operationResult);
            CustomResponseDto<NoContentDto> response = await _userApiService.CreateUserAsync(registerViewModel);
            if (response.IsSuccess)
            {
                operationResult.Success = true;
                operationResult.Message = "Please click on the confirmation link sent to your email address to complete the registration process!";
            }
            else
                operationResult.Errors = response.Errors;

            return Json(operationResult);
        }
        /// <summary>
        /// Confirm user Email
        /// </summary>
        /// <param name="SecretKey"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<IActionResult> ConfirmEmail(string SecretKey, string Email)
        {
            OperationResult operationResult = new();
            if (string.IsNullOrEmpty(SecretKey) || string.IsNullOrEmpty(Email))
                return Json(operationResult);
            var requestDto = new ConfirmUserMailDto() { SecretKey = SecretKey, Email = Email };
            var response = await _userApiService.ConfirmUserEmail(requestDto);
            operationResult.Success = response.IsSuccess;
            if (response.IsSuccess)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,response.Data.Email),
                        new Claim(ClaimTypes.Role,response.Data.Role),
                    };
                var useridentity = new ClaimsIdentity(claims, "AVS");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }
            else
                return RedirectToAction("Error", "Home");
            return Json(operationResult);
        }

        #region Forget My Password

        [HttpGet]
        public IActionResult ForgetMyPassword()
        {
            return View();
        }
        /// <summary>
        /// Send  reset password mail
        /// </summary>
        /// <param name="forgetPasswordDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ForgetMyPassword(ForgetPasswordDto forgetPasswordDto)
        {
            OperationResult operationResult = new();
            if (forgetPasswordDto == null)
                return Json(operationResult);
            var response = await _userApiService.ForgetMyPassword(forgetPasswordDto);
            operationResult.Success = response.IsSuccess;
            if (response.IsSuccess)
                operationResult.Message = "We have sent an e-mail to reset your password. You can complete your transaction with the link on the e-mail!";
            else
                operationResult.Errors = response.Errors;

            return Json(operationResult);
        }
        public async Task<IActionResult> ChangePasswordForForgotten(string SecretKey, string Email)
        {
            ViewBag.SecretKey = SecretKey;
            ViewBag.Email = Email;
            return View();
        }
        // Change user password
        [HttpPost]
        public async Task<IActionResult> ChangePasswordForForgotten(ChangePasswordForForgottenDto changePasswordForForgottenDto)
        {
            OperationResult operationResult = new();
            if (changePasswordForForgottenDto == null)
                return Json(operationResult);
            var response = await _userApiService.ChangePasswordForForgotten(changePasswordForForgottenDto);
            operationResult.Success = response.IsSuccess;
            if (response.IsSuccess)
                operationResult.Message = "Your password has been successfully updated.";
            else
                operationResult.Errors = response.Errors;

            return Json(operationResult);
        }
        #endregion

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
