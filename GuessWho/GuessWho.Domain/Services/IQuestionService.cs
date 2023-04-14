using GuessWho.Domain.Dtos;

namespace GuessWho.Domain.Services;

public interface IQuestionService
{
    Task<List<QuestionDto>> GetQuestionsAsync(int questionsCount, string language);
    Task AddQuestionsAsync(List<QuestionDto> questionDtos);
}