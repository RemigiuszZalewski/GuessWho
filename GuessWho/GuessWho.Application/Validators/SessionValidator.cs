using GuessWho.Domain.Entities;
using GuessWho.Domain.Enums;
using GuessWho.Domain.Validators;

namespace GuessWho.Application.Validators
{
    public class SessionValidator : ISessionValidator
    {
        private readonly int _numberOfPlayersThreshold = 6;

        public void ValidateSessionState(Session session)
        {
            var numberOfPlayers = session.Players.Count;

            if (session.SessionState != SessionState.Opened)
            {
                throw new Exception("Session has been already started.");
            }

            if (session.SessionState == SessionState.Opened && numberOfPlayers == _numberOfPlayersThreshold)
            {
                throw new Exception("Session has reached the maximum number of players");
            }
        }
    }
}
