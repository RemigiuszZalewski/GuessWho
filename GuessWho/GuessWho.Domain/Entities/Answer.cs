namespace GuessWho.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SessionId { get; set; }
        public int PlayerId { get; set; }
    }
}
