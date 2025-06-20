using System;
using Models;

namespace Services.Repositories.Interfaces;

public interface IUserRepository
{
    bool SaveUser(string email, string name, string password);
    public User SignIn(string email, string password);
}
