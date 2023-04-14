using Microsoft.EntityFrameworkCore;
using Worker.Contracts.Repositories;

namespace Worker.Persistence;

public class LogRepository : ILogRepository
{
    private readonly WorkerDbContext _context;

    public LogRepository(WorkerDbContext context)
    {
        _context = context;
    }

    public async Task<int> DeleteLogsAfterReachingDate(DateTime date)
    {
        var logs = _context.Logs.Where(x => x.Created < date);
        _context.Logs.RemoveRange(logs);
        await _context.SaveChangesAsync();
        return await logs.CountAsync();
    }
}