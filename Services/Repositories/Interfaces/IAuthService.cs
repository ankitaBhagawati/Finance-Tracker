using System;
using Models;
using Models.DTOs;

namespace Services.Repositories.Interfaces;

public interface IAuthService
{
    SignupResult Signup(SignupDTO dto);
    string? SignIn(SignInDTO dto);
    int GetUserID();
    User? GetUser();
}
