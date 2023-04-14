using Microsoft.Extensions.Options;
using Quartz;
using Worker.Contracts.Repositories;
using Worker.Options;

namespace Worker.Jobs;

[DisallowConcurrentExecution]
public class DeleteLogsJob : IJob
{
    private readonly ILogRepository _logRepository;
    private readonly DateTime _date;
    private readonly ILogger<DeleteLogsJob> _logger;
    
    public DeleteLogsJob(ILogRepository logRepository, IOptions<DeleteLogsJobOptions> options, ILogger<DeleteLogsJob> logger)
    {
        _logRepository = logRepository;
        _date = options.Value.DeleteAfterDate ??
                throw new ArgumentException("DeleteAfterDate was not found in configuration");
        _logger = logger;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("About to delete old logs from the database");
        var logsDeleted = await _logRepository.DeleteLogsAfterReachingDate(_date);
        _logger.LogInformation("Deleted {0} old logs from the database", logsDeleted.ToString());
    }
}