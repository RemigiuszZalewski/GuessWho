namespace GuessWho.Domain.Services;

public interface IEmailService
{
    Task SendEmailAsync(string emailAddress, string emailMessage);
}