using GuessWho.Domain.Entities;
using GuessWho.Domain.Enums;
using GuessWho.Domain.Generators;
using GuessWho.Domain.Repositories;
using GuessWho.Domain.Services;
using GuessWho.Domain.Validators;

namespace GuessWho.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IGenericRepository<Player> _playerRepository;
        private readonly ISessionCodeGenerator _sessionCodeGenerator;
        private readonly ISessionValidator _sessionValidator;

        public SessionService(ISessionRepository sessionRepository, IGenericRepository<Player> playerRepository,
            ISessionCodeGenerator sessionCodeGenerator, ISessionValidator sessionValidator)
        {
            _sessionRepository = sessionRepository;
            _playerRepository = playerRepository;
            _sessionCodeGenerator = sessionCodeGenerator;
            _sessionValidator = sessionValidator;
        }

        public async Task<string> CreateSession(int numberOfQuestions)
        {
            var sessionCode = _sessionCodeGenerator.GenerateSessionCode();

            var session = new Session
            {
                SessionCode = sessionCode
            };
            
            await _sessionRepository.AddAsync(session);

            return session.SessionCode;
        }

        public async Task JoinSession(string sessionCode, int playerId)
        {
            var session = await _sessionRepository.GetSessionBySessionCode(sessionCode);

            if (session is not null)
            {
                _sessionValidator.ValidateSessionState(session);
                var player = await _playerRepository.GetByIdAsync(playerId);

                if (player is null)
                    throw new Exception("Player does not exist.");
                
                session.Players.Add(player);
                await _sessionRepository.UpdateAsync(session);
            }
        }

        public async Task TerminateSession(string sessionCode)
        {
            var session = await _sessionRepository.GetSessionBySessionCode(sessionCode);

            if (session is not null)
            {
                session.SessionState = SessionState.Closed;
                await _sessionRepository.UpdateAsync(session);
            }
        }
    }
}