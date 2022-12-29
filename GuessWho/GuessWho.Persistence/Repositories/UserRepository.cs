using GuessWho.Domain.Entities;
using GuessWho.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GuessWho.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

        if (user is null)
        {
            throw new Exception("Invalid user name or password.");
        }

        return user;
    }
}