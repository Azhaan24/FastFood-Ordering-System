using FastFood.Core.DTOs.Auth;

namespace FastFood.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto model);

        Task<AuthResponseDto> LoginAsync(LoginDto model);

        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);

        Task LogoutAsync(string refreshToken);
    }
}