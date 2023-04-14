namespace Worker.Options;

public class DeleteLogsJobOptions
{
    public const string DeleteLogsJob = "DeleteLogsJob";
    public DateTime? DeleteAfterDate { get; set; }
}