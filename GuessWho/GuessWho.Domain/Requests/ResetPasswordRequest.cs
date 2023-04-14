namespace GuessWho.Domain.Requests;

public class ResetPasswordRequest
{
    public string Email { get; set; } = string.Empty;
}