using GuessWho.Application.Core.Abstractions;
using GuessWho.Domain.Dtos;
using GuessWho.Domain.Entities;
using GuessWho.Domain.Repositories;
using GuessWho.Domain.Requests;
using GuessWho.Domain.Services;

namespace GuessWho.Application.Services;

public class AccountService : IAccountService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public AccountService(IPasswordHasher passwordHasher, IJwtGenerator jwtGenerator,
        IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<UserDto> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

        var passwordIsValid = _passwordHasher.HashesMatch(user.HashedPassword, loginRequest.Password);

        if (!passwordIsValid)
        {
            throw new Exception("Invalid username or password.");
        }

        var jwt = _jwtGenerator.GenerateJwt(user);

        return new UserDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = jwt
        };
    }

    public async Task<UserDto> RegisterAsync(RegisterRequest registerRequest)
    {
        var passwordHash = _passwordHasher.HashPassword(registerRequest.Password);

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
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = jwt
        };
    }
}