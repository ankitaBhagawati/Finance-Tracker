using Azure;
using Dapper;
using Models;
using Services.Repositories.Interfaces;
using System.Data;

namespace Services.Repositories
{
    public class BudgetsRepository(IDbService dbService) : IBudgetsRepository
    {
        private readonly IDbService _dbService = dbService;

        public async Task<IEnumerable<Budgets>> GetAllBudgetByUserId(int user_id)
        {

            IEnumerable<Budgets> result = new List<Budgets>();
            try
            {
                using var connection = _dbService.GetConnection();
                string query = @"select
								budget_id,
                                user_name,
                                category_id,
                                category_name,
                                amount,
                                month,
                                year
								 from " + Constant.BudgetView;
                result = await connection.QueryAsync<Budgets>(query);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Budgets> GetBudgetById(int id)
        {
            Budgets? result = new Budgets();
            try
            {
                using var connection = _dbService.GetConnection();
                string query = @"select
								budget_id,
                                user_name,
                                category_name,
                                amount,
                                month,
                                year
								 from "+ Constant.BudgetView +
                                " Where budget_id = @Id";
                result = await connection.QueryFirstOrDefaultAsync<Budgets>(query, new { Id = id });
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateBudget(Budgets budget)
        {
            try
            {
                using var conn = _dbService.GetConnection();
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(
                    Constant.BudgetSP,  
                    new
                    {
                        sp_Operation = Constant.Add,
                        user_id = budget.user_id,
                        category_id = budget.category_id,
                        amount = budget.amount,
                        month = budget.month,
                        year = budget.year
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


        public async Task<bool> UpdateBudget(int id, Budgets budget)
        {
            try
            {
                using var conn = _dbService.GetConnection();
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(
                    Constant.BudgetSP,
                    new
                    {
                        sp_Operation = Constant.Update,
                        budget_id = id,
                        category_id = budget.category_id,
                        amount = budget.amount,
                        month = budget.month,
                        year = budget.year
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

        public async Task<bool> DeleteBudget(int id)
        {
            try
            {
                using var conn = _dbService.GetConnection();
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(
                    Constant.BudgetSP,
                    new
                    {
                        sp_Operation = Constant.Delete,
                        budget_id = id
                    },
                    commandType: CommandType.StoredProcedure
                );
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
