namespace GuessWho.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public List<Player> Players { get; set; }

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}