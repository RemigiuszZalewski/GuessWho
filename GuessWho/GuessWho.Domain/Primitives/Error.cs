namespace GuessWho.Domain.Primitives;

public class Error
{
    public string Code { get; set; }
    public string ErrorMessage { get; set; }
    
    public Error(string code, string errorMessage)
    {
        Code = code;
        ErrorMessage = errorMessage;
    }
}