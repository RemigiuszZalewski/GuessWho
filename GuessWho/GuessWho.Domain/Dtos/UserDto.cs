namespace GuessWho.Domain.Dtos;

public class UserDto
{
    public string FullName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}