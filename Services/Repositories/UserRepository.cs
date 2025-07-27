using System;
using System.Data;
using Dapper;
using Isopoh.Cryptography.Argon2;
using Models;
using Models.DTOs;
using Services.Repositories.Interfaces;

namespace Services.Repositories;

public class UserRepository(IDbService dbService) : IUserRepository
{
    private readonly IDbService _dbService = dbService;

    public int SaveUser(string email, string name, string password)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            conn.Open();

            var parameters = new DynamicParameters();
            parameters.Add("@name", name);
            parameters.Add("@email", email);
            parameters.Add("@password", password);
            parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute(Constant.UserSP, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@id");
        }
        catch (Exception ex)
        {
            return -1;
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
            SELECT Id, Email, Name, Password FROM Users WHERE Email = @Email",
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
            SELECT TOP(1) Email, Name FROM Users WHERE Id = @Id",
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
            var user = conn.QueryFirstOrDefault<User>(@"Select TOP(1) Email, Name, Password FROM Users WHERE email =@Email",
                new { Email = email });
            return user != null;

        }
        catch
        {
            return false;
        }
    }
}
