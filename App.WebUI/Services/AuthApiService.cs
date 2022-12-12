using App.Core.Dto;
using App.WebUI.Models;

namespace App.WebUI.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;
        private const string serviceName = "auth";

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomResponseDto<TokenDto>> Login(LoginViewModel loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync(string.Concat(serviceName, "/login"), loginDto);
            return await response.Content.ReadFromJsonAsync<CustomResponseDto<TokenDto>>();
        }
        public async Task<CustomResponseDto<TokenDto>> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var response = await _httpClient.PostAsJsonAsync(string.Concat(serviceName, "/CreateTokenByRefreshToken"), refreshTokenDto);
            return await response.Content.ReadFromJsonAsync<CustomResponseDto<TokenDto>>();
        }
        public async Task<CustomResponseDto<NoContentDto>> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var response = await _httpClient.PostAsJsonAsync(string.Concat(serviceName, "/RevokeRefreshToken"), refreshTokenDto);
            return await response.Content.ReadFromJsonAsync<CustomResponseDto<NoContentDto>>();
        }
    }
}
