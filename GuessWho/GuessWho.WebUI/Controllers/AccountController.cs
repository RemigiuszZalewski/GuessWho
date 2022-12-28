using GuessWho.Domain.Requests;
using GuessWho.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuessWho.WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var jwt = await _accountService.LoginAsync(loginRequest);

            return Ok(jwt);
        }

        [HttpPost]
        [Route("/Register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            return Ok(await _accountService.RegisterAsync(registerRequest));
        }
    }
}