using System;
using Dapper;
using Models;
using Services.Repositories.Interfaces;

namespace Services.Repositories;

public class UserRepository(IDbService dbService) : IUserRepository
{
    private readonly IDbService _dbService = dbService;

    public bool SaveUser(string email, string name, string password)
    {
        using var conn = _dbService.GetConnection();
        conn.Open();

        var result = conn.Query(@"
            EXEC sp_CreateUser @name = @Name, @email = @Email, @password = @Password
        ", new
        {
            Name = name,
            Email = email,
            Password = password,
        });

        return true;
    }
}
