using GuessWho.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuessWho.WebUI.Controllers
{
    [Authorize]
    public class SessionController : BaseController
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> CreateSession(int numberOfQuestions, string language)
        {
            return Ok(await _sessionService.CreateSession(numberOfQuestions, language));
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
