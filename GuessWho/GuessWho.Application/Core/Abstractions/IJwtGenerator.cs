using GuessWho.Domain.Entities;

namespace GuessWho.Application.Core.Abstractions;

public interface IJwtGenerator
{
    string GenerateJwt(User user);
}