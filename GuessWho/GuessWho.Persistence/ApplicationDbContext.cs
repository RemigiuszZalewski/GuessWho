using GuessWho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GuessWho.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        DbSet<Player> Players { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<Answer> Answers { get; set; }
    }
}
