using System;
using Models;

namespace Services.Repositories.Interfaces;

public interface IUserRepository
{
    int SaveUser(string email, string name, string password);
    User? SignIn(string email, string password);
    User? GetUser(int id);
    public bool isExists(string email);
}
