using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTOs;
using Services.Repositories.Interfaces;

namespace Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public bool Signup(SignupDTO dto)
    {
        // TODO: validation
        string hashedPassword = Argon2.Hash(dto.Password);
        var created = _userRepository.SaveUser(dto.Email, dto.Name, hashedPassword);

        return created;
    }

    public string? SignIn(SignInDTO dto)
    {
        var user = _userRepository.SignIn(dto.Email, dto.Password);
        if (user == null)
        {
            return null;
        }

        var token = GenerateJwtToken(user.Id);

        return token;
    }

    private string GenerateJwtToken(int id)
    {
        var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, id.ToString()) };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetValue<string>("jwtSecret")!)
        );
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(365),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
