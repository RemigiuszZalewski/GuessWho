using GuessWho.Domain.Generators;

namespace GuessWho.Application.Generators;

public class SessionCodeGenerator : ISessionCodeGenerator
{
    private const string Chars = "ABCDEFGHIJKLMNOPRSTUWXYZ0123456789!#$@_";
    private const int NumberOfCodeCharacters = 10;

    private static readonly Random Random = new();

    public string GenerateSessionCode()
    {
        return new string(Enumerable.Repeat(Chars, NumberOfCodeCharacters)
            .Select(x => x[Random.Next(x.Length)]).ToArray());
    }
}