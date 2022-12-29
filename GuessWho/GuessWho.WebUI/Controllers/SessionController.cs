using GuessWho.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessWho.WebUI.Controllers
{
    public class SessionController : BaseController
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> CreateSession(int numberOfQuestions)
        {
            return Ok(await _sessionService.CreateSession(numberOfQuestions));
        }

        [HttpPatch]
        [Route("Join")]
        public async Task<ActionResult> JoinSession([FromQuery] string sessionCode, [FromQuery] int playerId)
        {
            await _sessionService.JoinSession(sessionCode, playerId);
            return Ok();
        }

        [HttpPatch]
        [Route("Terminate")]
        public async Task<IActionResult> TerminateSession(string sessionCode)
        {
            await _sessionService.TerminateSession(sessionCode);
            return Ok();
        }
    }
}
