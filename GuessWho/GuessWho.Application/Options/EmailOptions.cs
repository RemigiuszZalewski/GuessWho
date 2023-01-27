namespace GuessWho.Application.Options;

public class EmailOptions
{
    public const string OptionsKey = "SendEmailOptions";
    public string Email { get; set; }
    public string Password { get; set; }
}