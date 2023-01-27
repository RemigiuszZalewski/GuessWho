using GuessWho.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuessWho.WebUI.Controllers
{
    [Authorize]
    public class PlayerController : BaseController
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> CreatePlayer([FromQuery] string name, [FromQuery] string sessionCode)
        {
            return Ok(await _playerService.CreatePlayerAsync(name, sessionCode));
        }
    }
}
