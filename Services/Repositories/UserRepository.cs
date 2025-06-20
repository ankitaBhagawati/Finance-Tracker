using System;
using Dapper;
using Models;
using Models.DTOs;
using Org.BouncyCastle.Crypto.Generators;
using Services.Repositories.Interfaces;


namespace Services.Repositories;

public class UserRepository(IDbService dbService) : IUserRepository
{
    private readonly IDbService _dbService = dbService;

    public bool SaveUser(string email, string name, string password)
    {
        try
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
        catch (Exception ex)
        {
            return false;
        }
    }
    public User SignIn(string email, string password)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            conn.Open();

            var user = conn.QueryFirstOrDefault<User>(@"
            SELECT * FROM Users WHERE Email = @Email",
                new { Email = email });

            if (user == null)
                return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return isPasswordValid ? user : null;
        }
        catch
        {
            return null;
        }
    }

}
