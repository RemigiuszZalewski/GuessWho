using System.Text;
using GuessWho.Domain.Generators;

namespace GuessWho.Application.Generators;

public class EmailGenerator : IEmailGenerator
{
    public string GenerateResetPasswordEmailMessage(string fullName, string token)
    {
        var sb = new StringBuilder();
        sb.Append($"Hi {fullName}! <br/><br/>");
        sb.Append("There was an attempt to reset password in GuessWho. We hope that is was you 😀</br></br>");
        sb.Append("Please click the following link to reset the password:</br>");
        sb.Append($"https://guesswho.fly.dev/change-password?token={token}</br></br>");
        sb.Append("Please ignore that email if you haven't requested for password reset in our service 😊");

        return sb.ToString();
    }
}