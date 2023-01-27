using System.Net;
using System.Net.Mail;
using GuessWho.Application.Options;
using GuessWho.Domain.Generators;
using GuessWho.Domain.Services;
using Microsoft.Extensions.Options;

namespace GuessWho.Application.Services;

public class EmailService : IEmailService
{
    private readonly IEmailGenerator _emailGenerator;
    private readonly EmailOptions _emailOptions;

    public EmailService(IEmailGenerator emailGenerator, IOptions<EmailOptions> emailOptions)
    {
        _emailGenerator = emailGenerator;
        _emailOptions = emailOptions.Value;
    }
    
    public async Task SendEmailAsync(string emailAddress, string emailMessage)
    {
        var email = new MailMessage();
        email.From = new MailAddress(_emailOptions.Email);
        email.Subject = "Reset Password";
        email.To.Add(new MailAddress(emailAddress));
        email.Body = $"<html><body>{emailMessage}</body></html>";
        email.IsBodyHtml = true;

        var smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new NetworkCredential(_emailOptions.Email, _emailOptions.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(email);
    }
}