namespace Worker.Contracts.Entities;

public class Log
{
    public int Id { get; set; }
    public string? Stacktrace { get; set; }
    public string? Message { get; set; }
    public LogLevel LogLevel { get; set; }
    public DateTime Created { get; set; }
}