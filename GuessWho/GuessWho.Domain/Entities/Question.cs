namespace GuessWho.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
    }
}
