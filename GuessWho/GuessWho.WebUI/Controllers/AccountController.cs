using GuessWho.Domain.Requests;
using GuessWho.Domain.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            return Ok(await _accountService.LoginAsync(loginRequest));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            return Ok(await _accountService.RegisterAsync(registerRequest));
        }
        
        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"] 
                               ?? throw new Exception("No refresh token available.");
            var email = User.Identity?.Name ??
                         throw new Exception("No email in JWT found.");
            
            return Ok(await _accountService.RefreshTokenAsync(refreshToken, email));
        }
        
        [Authorize]
        [HttpPatch("password/edit")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
        {
            await _accountService.ChangePasswordAsync(changePasswordRequest);
            return Ok();
        }
        
        [HttpPost("password/reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            await _accountService.ResetPasswordAsync(resetPasswordRequest.Email);
            return Ok();
        }
    }
}