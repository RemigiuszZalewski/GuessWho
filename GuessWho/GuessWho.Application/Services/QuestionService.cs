using GuessWho.Domain.Dtos;
using GuessWho.Domain.Entities;
using GuessWho.Domain.Repositories;
using GuessWho.Domain.Services;
using Mapster;

namespace GuessWho.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }
    
    public async Task<List<QuestionDto>> GetQuestionsAsync(int questionsCount, string language)
    {
        var questions = await _questionRepository.GetQuestionsAsync(questionsCount, language);
        return questions.Adapt<List<QuestionDto>>();
    }

    public async Task AddQuestionsAsync(List<QuestionDto> questionDtos)
    {
        var questions = questionDtos.Adapt<List<Question>>();
        await _questionRepository.AddRangeAsync(questions);
    }
}