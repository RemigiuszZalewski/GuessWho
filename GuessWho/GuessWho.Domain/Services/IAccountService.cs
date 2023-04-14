using GuessWho.Domain.Dtos;
using GuessWho.Domain.Requests;

namespace GuessWho.Domain.Services;

public interface IAccountService
{
    Task<UserDto> LoginAsync(LoginRequest loginRequest);
    Task<UserDto> RegisterAsync(RegisterRequest registerRequest);
    Task<string> RefreshTokenAsync(string refreshToken, string email);
    Task ChangePasswordAsync(ChangePasswordRequest changePasswordRequest);
    Task ResetPasswordAsync(string email);
}