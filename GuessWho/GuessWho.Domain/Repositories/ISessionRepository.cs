using GuessWho.Domain.Entities;

namespace GuessWho.Domain.Repositories;

public interface ISessionRepository : IGenericRepository<Session>
{
    Task<Session?> GetSessionBySessionCode(string sessionCode);
}