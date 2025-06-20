using Isopoh.Cryptography.Argon2;
using Models;
using Models.DTOs;
using Services.Repositories.Interfaces;

namespace Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Signup(SignupDTO dto)
    {
        // TODO: validation
        string hashedPassword = Argon2.Hash(dto.Password);
        var created = _userRepository.SaveUser(dto.Email, dto.Name, hashedPassword);

        return created;
    }
    public bool SignIn(SignInDTO dto)
    {
        string hashedPassword = Argon2.Hash(dto.Password);
        var authenticate = _userRepository.SignIn(dto.Email, hashedPassword);
        if (authenticate == null)
        {
            return false;
        }
        return true;
    }
}
