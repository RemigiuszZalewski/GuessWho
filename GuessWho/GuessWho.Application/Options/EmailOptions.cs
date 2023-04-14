namespace GuessWho.Application.Options;

public class EmailOptions
{
    public const string OptionsKey = "SendEmailOptions";
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}