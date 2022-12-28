namespace GuessWho.Application.Core.Abstractions;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool HashesMatch(string hash, string providedPassword);
}