using GuessWho.Domain.Entities;

namespace GuessWho.Domain.Services;

public interface IPlayerService
{
    Task<int> CreatePlayerAsync(string name, string sessionCode);
}