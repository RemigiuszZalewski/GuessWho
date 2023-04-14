using GuessWho.Domain.Dtos;
using GuessWho.Domain.Entities;
using GuessWho.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace GuessWho.Persistence.Repositories;

public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
{
    private readonly ApplicationDbContext _context;
    
    public QuestionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetQuestionsAsync(int questionsCount, string language)
    {
        var questions = await _context.Questions.Where(x => x.Language.Equals(language))
            .OrderBy(x => Guid.NewGuid())
            .Take(questionsCount)
            .ToListAsync();

        return questions;
    }
}