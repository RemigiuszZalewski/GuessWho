using GuessWho.Domain.Entities;

namespace GuessWho.Domain.Repositories;

public interface IQuestionRepository : IGenericRepository<Question>
{
    Task<List<Question>> GetQuestionsAsync(int questionsCount, string language);
}