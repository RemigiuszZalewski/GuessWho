namespace GuessWho.Domain.Services
{
    public interface ISessionService
    {
        Task<string> CreateSession(int numberOfQuestions, string language);
        Task JoinSession(string sessionCode, int playerId);
        Task TerminateSession(string sessionCode);
    }
}
