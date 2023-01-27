namespace GuessWho.Application.Core.Abstractions;

public interface IStringHasher
{
    string Hash(string password);
    bool HashesMatch(string hash, string providedPassword);
}