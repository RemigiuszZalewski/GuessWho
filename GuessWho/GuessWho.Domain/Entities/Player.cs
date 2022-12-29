namespace GuessWho.Domain.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Player(string name)
        {
            Name = name;
        }
    }
}
