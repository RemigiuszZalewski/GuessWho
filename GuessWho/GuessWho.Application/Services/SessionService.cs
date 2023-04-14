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
        private readonly IQuestionRepository _questionRepository;
        private readonly ISessionValidator _sessionValidator;

        public SessionService(ISessionRepository sessionRepository, IGenericRepository<Player> playerRepository,
            ISessionCodeGenerator sessionCodeGenerator, IQuestionRepository questionRepository,
            ISessionValidator sessionValidator)
        {
            _sessionRepository = sessionRepository;
            _playerRepository = playerRepository;
            _sessionCodeGenerator = sessionCodeGenerator;
            _questionRepository = questionRepository;
            _sessionValidator = sessionValidator;
        }

        public async Task<string> CreateSession(int numberOfQuestions, string language)
        {
            var sessionCode = _sessionCodeGenerator.GenerateSessionCode();

            var questions = await _questionRepository.GetQuestionsAsync(numberOfQuestions, "EN");
            
            var session = new Session
            {
                SessionCode = sessionCode,
                Questions = questions
            };
            
            await _sessionRepository.AddAsync(session);

            return session.SessionCode;
        }

        public async Task JoinSession(string sessionCode, int playerId)
        {
            var session = await _sessionRepository.GetSessionBySessionCode(sessionCode);

            _sessionValidator.ValidateSessionState(session);
            var player = await _playerRepository.GetByIdAsync(playerId);

            if (player is null)
                throw new Exception("Player does not exist.");
                
            session.Players.Add(player);
            await _sessionRepository.UpdateAsync(session);
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