using GuessWho.Domain.Requests;

namespace GuessWho.Domain.Services;

public interface IAccountService
{
    Task<string> LoginAsync(LoginRequest loginRequest);
    Task<int> RegisterAsync(RegisterRequest registerRequest);
}