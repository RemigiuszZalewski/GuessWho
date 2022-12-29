using GuessWho.Domain.Entities;
using GuessWho.Domain.Enums;

namespace GuessWho.Domain.Validators
{
    public interface ISessionValidator
    {
        void ValidateSessionState(Session session);
    }
}
