using GuessWho.Domain.Entities;
using GuessWho.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GuessWho.Persistence.Repositories;

public class SessionRepository : GenericRepository<Session>, ISessionRepository
{
    private readonly ApplicationDbContext _context;
    
    public SessionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Session?> GetSessionBySessionCode(string sessionCode)
    {
        var session = await _context.Sessions.FirstOrDefaultAsync(s => s.SessionCode.Equals(sessionCode));

        return session ?? throw new Exception("Session does not exist.");
    }
}