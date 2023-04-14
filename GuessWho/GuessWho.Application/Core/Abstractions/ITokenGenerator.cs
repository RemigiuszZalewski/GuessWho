using GuessWho.Domain.Entities;
using GuessWho.Domain.Primitives;

namespace GuessWho.Application.Core.Abstractions;

public interface ITokenGenerator
{
    string GenerateJwt(User user);
    RefreshToken GenerateRefreshToken();
}