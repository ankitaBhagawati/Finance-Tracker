using System;
using Dapper;
using Isopoh.Cryptography.Argon2;
using Models;
using Models.DTOs;
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

            var result = conn.Query(
                @"EXEC @SP_User @name = @Name, @email = @Email, @password = @Password
        ",
                new
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    SP_User = Constant.UserSP,
                }
            );

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public User? SignIn(string email, string password)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            conn.Open();

            var user = conn.QueryFirstOrDefault<User>(
                @"
            SELECT * FROM Users WHERE Email = @Email",
                new { Email = email }
            );

            if (user == null)
                return null;

            bool isPasswordValid = Argon2.Verify(user.Password, password);
            return isPasswordValid ? user : null;
        }
        catch
        {
            return null;
        }
    }

    public User? GetUser(int id)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            conn.Open();

            var user = conn.QueryFirstOrDefault<User>(
                @"
            SELECT TOP(1) * FROM Users WHERE Id = @Id",
                new { Id = id }
            );

            return user;
        }
        catch
        {
            return null;
        }
    }
    public bool isExists(string email)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            conn.Open();
            var user = conn.QueryFirstOrDefault<User>(@"Select TOP(1) * FROM Users WHERE email =@Email",
                new { Email = email });
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
