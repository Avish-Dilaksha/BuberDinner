using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Presistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // 1.) Validate user doesn;t exists
        if(_userRepository.GetUserByEmail(email) is not null){
            return Errors.User.DuplicateEmail;
        }

        // 2.) Create user (generate unique Id) & Presist to DB
        var user = new User {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);

        // 3.) Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user, 
            token
            );
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1.) Validate the user already exists
        if(_userRepository.GetUserByEmail(email) is not User user) {
            return Errors.Authentication.InvalidCredentials;
        }

        // 2.) Validate the Password is correct
        if(user.Password != password){
            return Errors.Authentication.InvalidCredentials;
        }

        // 3.) Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token
            );
    }
}