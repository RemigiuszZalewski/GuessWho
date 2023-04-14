using GuessWho.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessWho.Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public string SessionCode { get; set; } = string.Empty;
        public SessionState SessionState { get; set; }
        public int NumberOfQuestions { get; set; }
        public DateTime Created { get; set; }
        public List<Question> Questions { get; set; } = new();
        public List<Player> Players { get; set; } = new();
    }
}
