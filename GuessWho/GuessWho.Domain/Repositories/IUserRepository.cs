using GuessWho.Domain.Entities;

namespace GuessWho.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}