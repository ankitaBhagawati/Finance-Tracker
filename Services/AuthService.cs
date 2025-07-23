using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;

    }

    public SignupResult Signup(SignupDTO dto)
    {
        //Checks if the email already exists in our db
        if (_userRepository.isExists(dto.Email))
        {
            return new SignupResult
            {
                Success = false,
                Message = "User already exists"
            };
        }
        string hashedPassword = Argon2.Hash(dto.Password);
        bool created = _userRepository.SaveUser(dto.Email, dto.Name, hashedPassword);

        return new SignupResult
        {
            Success = created,
            Message = created ? "User created" : "Failed to create user"
        };
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
    public int GetUserID()
    {
        var sub = _httpContextAccessor
            .HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;
        if (sub == null)
        {
            throw new UnauthorizedAccessException("User ID not found in token.");
        }
        if (!int.TryParse(sub, out int userId))
        {
            throw new UnauthorizedAccessException("Invalid User ID in token.");
        }
        return userId;
    }

    public User? GetUser()
    {
        var userId = GetUserID();
        return _userRepository.GetUser(userId);
    }
}
