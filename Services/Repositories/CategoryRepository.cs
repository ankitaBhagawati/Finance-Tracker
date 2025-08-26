using Models;
using Dapper;
using Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Transactions;

namespace Services.Repositories
{
    public class CategoryRepository(IDbService dbService) : ICategoryRepository
    {
        private readonly IDbService _dbService = dbService;
    
        public async Task<IEnumerable<Category>> GetCategories()
        {
            IEnumerable<Category> result = new List<Category>();
            try
            {
                using var connection = _dbService.GetConnection();
                string query = @"select
                                    Category_Id,
                                    Category_Name,
                                    isActive
                                 from " + Constant.CategoryTable;
                result = await connection.QueryAsync<Category>(query);
                return result;
            }
            catch (Exception)
            {
                throw;
            } 
        }
        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                using var conn = _dbService.GetConnection();
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(
                    Constant.CategorySP,
                    new
                    {
                        sp_Operation = Constant.Add,
                        Category_Id= 0,
                        Category_Name = category.Category_Name
                    },
                    commandType: CommandType.StoredProcedure
                );

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> UpdateCategory(Category category)
        {
            try
            {
                using var conn = _dbService.GetConnection();
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(
                    Constant.CategorySP,
                    new
                    {
                        sp_Operation = Constant.Update,
                        Category_Id = category.Category_Id,
                        Category_Name = category.Category_Name
                    },
                    commandType: CommandType.StoredProcedure
                );

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                using var conn = _dbService.GetConnection();
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(
                    Constant.CategorySP,
                    new
                    {
                        sp_Operation = Constant.Delete,
                        Category_Id = id,
                        Category_Name= ""
                    },
                    commandType: CommandType.StoredProcedure
                );

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
