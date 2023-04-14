namespace GuessWho.Infrastructure.Authentication;

public class JwtOptions
{
    public const string OptionsKey = "JwtOptions";
    
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecurityKey { get; set; }
    public int JwtTokenExpirationTimeInMinutes { get; set; }
    public int RefreshTokenExpirationTimeInHours { get; set; }
    
    public JwtOptions()
    {
        Issuer = string.Empty;
        Audience = string.Empty;
        SecurityKey = string.Empty;
    }
}