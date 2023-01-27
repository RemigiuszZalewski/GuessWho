namespace GuessWho.Domain.Entities;

public class ResetPasswordToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string TokenHash { get; set; }
    public DateTime? ExpirationTime { get; set; }
}