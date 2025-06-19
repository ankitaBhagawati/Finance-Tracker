using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Services.Repositories.Interfaces;

namespace Services.Repositories;

public class DbService : IDbService
{
    private IConfiguration _configuration;

    public DbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public SqlConnection GetConnection()
    {
        Console.WriteLine(_configuration.GetConnectionString("Primary"));
        return new SqlConnection(_configuration.GetConnectionString("Primary"));
    }
}
