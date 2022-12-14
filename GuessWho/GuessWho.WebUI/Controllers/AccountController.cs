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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            return Ok(await _accountService.LoginAsync(loginRequest));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            return Ok(await _accountService.RegisterAsync(registerRequest));
        }
    }
}