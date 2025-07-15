using Dapper;
using Models;
using Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories
{
    public class TransactionRepository(IDbService dbService): ITransactionRepository
    {
        private readonly IDbService _dbService = dbService;

    public async Task<IEnumerable<Transaction>> GetAllTransactionByUserId(int user_id)
    {
        IEnumerable<Transaction> result = new List<Transaction>();
        try
        {
            using var connection = _dbService.GetConnection();
            string query = @"select
                                    transaction_id,
                                    user_name,
                                    category_name,
                                    amount,
                                    description,
                                    transaction_date
                                 from " + Constant.TransactionView;
            result = await connection.QueryAsync<Transaction>(query);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Transaction> GetTransactionById(int id)
    {
        Transaction? result = new Transaction();
        try
        {
            using var connection = _dbService.GetConnection();
            string query = @"select
                                    transaction_id,
                                    user_name,
                                    category_name,
                                    amount,
                                    description,
                                    transaction_date
                                 from " + Constant.TransactionView +
                            " Where transaction_id = @Id";
            result = await connection.QueryFirstOrDefaultAsync<Transaction>(query, new { Id = id });
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<bool> CreateTransaction(Transaction transaction)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            await conn.OpenAsync();

            var result = await conn.ExecuteAsync(
                Constant.TransactionSP,
                new
                {
                    sp_Operation = Constant.Add,
                    user_id = transaction.user_id,
                    category_id = transaction.category_id,
                    amount = transaction.amount,
                    description = transaction.description,
                    transaction_date = transaction.transaction_date
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
    public async Task<bool> UpdateTransaction(int id, Transaction transaction)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            await conn.OpenAsync();

            var result = await conn.ExecuteAsync(
                Constant.TransactionSP,
                new
                {
                    sp_Operation = Constant.Update,
                    transaction_id = id,
                    category_id = transaction.category_id,
                    amount = transaction.amount,
                    description = transaction.description,
                    transaction_date = transaction.transaction_date
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

    public async Task<bool> DeleteTransaction(int id)
    {
        try
        {
            using var conn = _dbService.GetConnection();
            await conn.OpenAsync();

            var result = await conn.ExecuteAsync(
                Constant.TransactionSP,
                new
                {
                    sp_Operation = Constant.Delete,
                    transaction_id = id
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