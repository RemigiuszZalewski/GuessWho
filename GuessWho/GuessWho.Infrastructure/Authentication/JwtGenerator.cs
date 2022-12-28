using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GuessWho.Application.Core.Abstractions;
using GuessWho.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GuessWho.Infrastructure.Authentication;

public class JwtGenerator : IJwtGenerator
{
    private readonly JwtOptions _jwtOptions;

    public JwtGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName)
        };

        var tokenExpirationTime = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes);

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }
}