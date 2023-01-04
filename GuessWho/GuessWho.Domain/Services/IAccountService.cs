using GuessWho.Domain.Dtos;
using GuessWho.Domain.Requests;

namespace GuessWho.Domain.Services;

public interface IAccountService
{
    Task<UserDto> LoginAsync(LoginRequest loginRequest);
    Task<UserDto> RegisterAsync(RegisterRequest registerRequest);
}