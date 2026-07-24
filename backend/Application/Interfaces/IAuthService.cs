using CadernosDeErros.DTOs;

namespace CadernosDeErros.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserProfileDto?> GetUserProfileAsync(int userId);
    }
}
