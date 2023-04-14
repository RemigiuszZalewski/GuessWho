using Bogus;
using Microsoft.EntityFrameworkCore;
using Worker.Contracts.Entities;

namespace Worker.Persistence;

public static class Seeder
{
    private static List<string> _exceptionMessages =
        new() { "MessageFromException1", "MessageFromException2", "MessageFromException3", "MessageFromException4" };

    private static List<string> _stackTrace = new() { "StackTrace1", "StackTrace2", "StackTrace3", "StackTrace4" };
    
    public static void Seed(WorkerDbContext dbContext)
    {
        if (dbContext.Logs.Count() >= 100) return;
        
        var faker = new Faker<Log>().RuleFor(x => x.Created, x => x.Date.Past())
            .RuleFor(x => x.Stacktrace, x => x.PickRandom(_exceptionMessages))
            .RuleFor(x => x.Message, x => x.PickRandom(_stackTrace))
            .RuleFor(x => x.LogLevel, x => x.PickRandom<LogLevel>());

        var fakeData = faker.GenerateBetween(200,200);
        
        dbContext.Logs.AddRange(fakeData);
        dbContext.SaveChanges();
    }
}