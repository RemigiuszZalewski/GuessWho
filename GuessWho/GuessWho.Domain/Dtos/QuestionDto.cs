using GuessWho.Domain.Entities;

namespace GuessWho.Domain.Dtos;

public class QuestionDto : BaseDto<QuestionDto, Question>
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
}