using App.Core.Dto;
using App.WebUI.Models;

namespace App.WebUI.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        private const string serviceName = "user";

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Kullanıcı ekle
        public async Task<CustomResponseDto<NoContentDto>> CreateUserAsync(RegisterViewModel userCreateDto)
        {
            var response = await _httpClient.PostAsJsonAsync(serviceName, userCreateDto);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return CustomResponseDto<NoContentDto>.Success(204);
            else
                return CustomResponseDto<NoContentDto>.Fail(400, "There is a user for this email. If you forgot your password, use the password usage step!");
        }
        // Üye olduktan sonra gönderilen email ile üyelik onaylanır
        public async Task<CustomResponseDto<UserDto>> ConfirmUserEmail(ConfirmUserMailDto confirmUserMailDto)
        {
            var response = await _httpClient.PostAsJsonAsync(String.Concat(serviceName, "/ConfirmEmail"), confirmUserMailDto);
            return await response.Content.ReadFromJsonAsync<CustomResponseDto<UserDto>>();
        }
        // Kullanıcı kayıt raporları
        public async Task<CustomResponseDto<UserRegisterationReportResponseDto>> UserRegisterationReports(UserRegisterationReportRequestDto userRegisterationReportRequestDto)
        {
            var response = await _httpClient.PostAsJsonAsync(String.Concat(serviceName, "/UserRegisterationReports"), userRegisterationReportRequestDto);
            return await response.Content.ReadFromJsonAsync<CustomResponseDto<UserRegisterationReportResponseDto>>();
        }
        // Şifremi unuttum ile yeni şifre oluşturma
        public async Task<CustomResponseDto<NoContentDto>> ChangePasswordForForgotten(ChangePasswordForForgottenDto changePasswordForForgottenDto)
        {
            var response = await _httpClient.PostAsJsonAsync(String.Concat(serviceName, "/ChangePasswordForForgotten"), changePasswordForForgottenDto);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return CustomResponseDto<NoContentDto>.Success(204);
            else
                return CustomResponseDto<NoContentDto>.Fail(400, "Bir hata oluştu!");
        }
        // Unutulan şifre için mail gönder
        public async Task<CustomResponseDto<NoContentDto>> ForgetMyPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var response = await _httpClient.PostAsJsonAsync(String.Concat(serviceName, "/ForgetMyPassword"), forgetPasswordDto);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return CustomResponseDto<NoContentDto>.Success(204);
            else
                return CustomResponseDto<NoContentDto>.Fail(400, "Bir hata oluştu!");

        }

    }
}
