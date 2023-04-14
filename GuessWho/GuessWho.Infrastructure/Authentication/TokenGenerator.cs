using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GuessWho.Application.Core.Abstractions;
using GuessWho.Domain.Entities;
using GuessWho.Domain.Primitives;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GuessWho.Infrastructure.Authentication;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _jwtOptions;

    public TokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims =
        {
            new ("userId", user.Id.ToString()),
            new ("name", user.Email),
        };

        var jwtTokenExpirationTime = DateTimeOffset.Now.AddHours(_jwtOptions.JwtTokenExpirationTimeInMinutes);

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            jwtTokenExpirationTime.DateTime,
            signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }

    public RefreshToken GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            TokenExpirationDate = DateTimeOffset.Now.AddHours(_jwtOptions.RefreshTokenExpirationTimeInHours)
        };
    }
}