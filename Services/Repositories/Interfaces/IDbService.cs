using System;
using Microsoft.Data.SqlClient;

namespace Services.Repositories.Interfaces;

public interface IDbService
{
    SqlConnection GetConnection();
}
