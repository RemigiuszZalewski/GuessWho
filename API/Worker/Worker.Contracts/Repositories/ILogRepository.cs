namespace Worker.Contracts.Repositories;

public interface ILogRepository
{
    Task<int> DeleteLogsAfterReachingDate(DateTime date);
}