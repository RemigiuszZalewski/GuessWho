using GuessWho.Application.Core.Abstractions;
using GuessWho.Domain.Dtos;
using GuessWho.Domain.Entities;
using GuessWho.Domain.Generators;
using GuessWho.Domain.Repositories;
using GuessWho.Domain.Requests;
using GuessWho.Domain.Services;

namespace GuessWho.Application.Services;

public class AccountService : IAccountService
{
    private readonly IStringHasher _stringHasher;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IEmailGenerator _emailGenerator;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;
    private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;

    public AccountService(IStringHasher stringHasher, IJwtGenerator jwtGenerator,
        IEmailGenerator emailGenerator, IEmailService emailService, IUserRepository userRepository,
        IResetPasswordTokenRepository resetPasswordTokenRepository)
    {
        _stringHasher = stringHasher;
        _jwtGenerator = jwtGenerator;
        _emailGenerator = emailGenerator;
        _emailService = emailService;
        _userRepository = userRepository;
        _resetPasswordTokenRepository = resetPasswordTokenRepository;
    }
    
    public async Task<UserDto> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);
        var passwordIsValid = _stringHasher.HashesMatch(user.HashedPassword, loginRequest.Password);

        if (!passwordIsValid)
        {
            throw new Exception("Invalid username or password.");
        }

        var jwt = _jwtGenerator.GenerateJwt(user);

        return new UserDto
        {
            FullName = user.FirstName + " " + user.LastName,
            UserId = user.Id,
            Token = jwt
        };
    }

    public async Task<UserDto> RegisterAsync(RegisterRequest registerRequest)
    {
        var passwordHash = _stringHasher.Hash(registerRequest.Password);

        var user = new User
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            HashedPassword = passwordHash,
            Email = registerRequest.Email
        };

        await _userRepository.AddAsync(user);
        var jwt = _jwtGenerator.GenerateJwt(user);

        return new UserDto
        {
            FullName = user.ToString(),
            UserId = user.Id,
            Token = jwt
        };
    }

    public async Task ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
    {
        var user = await _userRepository.GetByIdAsync(changePasswordRequest.UserId);
        var oldPasswordHash = _stringHasher.Hash(changePasswordRequest.OldPassword);

        if (!user.HashedPassword.Equals(oldPasswordHash))
        {
            throw new Exception("Old password is not correct.");
        }

        var newPasswordHash = _stringHasher.Hash(changePasswordRequest.NewPassword);
        user.HashedPassword = newPasswordHash;

        await _userRepository.UpdateAsync(user);
    }

    public async Task ResetPasswordAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var token = _stringHasher.Hash($"ResetPasswordTemporaryString+{user.Email}+{DateTime.Now}");

        await _resetPasswordTokenRepository.AddAsync(new ResetPasswordToken
        {
            UserId = user.Id,
            ExpirationTime = DateTime.Now.AddDays(1),
            TokenHash = token
        });
        
        var resetPasswordEmailMessage = 
            _emailGenerator.GenerateResetPasswordEmailMessage(user.ToString(), token);
        
        await _emailService.SendEmailAsync(email, resetPasswordEmailMessage);
    }
}