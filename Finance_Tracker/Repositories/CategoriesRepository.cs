using Dapper;
using Finance_Tracker.Interfaces;
using Finance_Tracker.Models;
using System.Data;
using System.Data.SqlClient;

namespace Finance_Tracker.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly string _connectionString;

        public CategoriesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Categories>> GetAllCategories()
        {
            try
            {
                using var connection = CreateConnection();
                var query = "SELECT [category_id], [category_name], [IsActive] " +
                            "FROM Categories WHERE IsActive = 1";
                return await connection.QueryAsync<Categories>(query);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving category data.", ex);
            }
        }

        public async Task<Categories> GetCategoryById(int id)
        {
            using var connection = CreateConnection();
            var query = "SELECT [category_id], [category_name], [IsActive] " +
                        "FROM Categories WHERE category_id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Categories>(query, new { Id = id });
        }
    }
}
