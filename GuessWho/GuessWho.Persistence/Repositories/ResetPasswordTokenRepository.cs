using GuessWho.Domain.Entities;
using GuessWho.Domain.Repositories;

namespace GuessWho.Persistence.Repositories;

public class ResetPasswordTokenRepository : GenericRepository<ResetPasswordToken>, IResetPasswordTokenRepository
{
    private readonly ApplicationDbContext _context;
    
    public ResetPasswordTokenRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}