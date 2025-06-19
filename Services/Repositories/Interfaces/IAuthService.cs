using System;
using Models;
using Models.DTOs;

namespace Services.Repositories.Interfaces;

public interface IAuthService
{
    bool Signup(SignupDTO dto);
}
