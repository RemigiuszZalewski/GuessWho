using GuessWho.Domain.Entities;
using GuessWho.Domain.Repositories;
using GuessWho.Domain.Services;

namespace GuessWho.Application.Services;

public class PlayerService : IPlayerService
{
    private readonly IGenericRepository<Player> _playerRepository;
    private readonly ISessionRepository _sessionRepository;

    public PlayerService(IGenericRepository<Player> playerRepository, ISessionRepository sessionRepository)
    {
        _playerRepository = playerRepository;
        _sessionRepository = sessionRepository;
    }
    
    public async Task<int> CreatePlayerAsync(string name, string sessionCode)
    {
        var player = new Player(name);
        
        await _playerRepository.AddAsync(player);
        var session = await _sessionRepository.GetSessionBySessionCode(sessionCode);
        session.Players.Add(player);
        await _sessionRepository.UpdateAsync(session);
        return player.Id;
    }
}