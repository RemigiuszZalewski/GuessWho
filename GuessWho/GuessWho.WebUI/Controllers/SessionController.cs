using Microsoft.AspNetCore.Mvc;

namespace GuessWho.WebUI.Controllers
{
    public class SessionController : BaseController
    {
        [HttpPost]
        public ActionResult<int> CreateSession()
        {
            return 1;
        }

        [HttpPost]
        public ActionResult JoinSession(string sessionCode, int playerId)
        {
            return Ok();
        }
    }
}
