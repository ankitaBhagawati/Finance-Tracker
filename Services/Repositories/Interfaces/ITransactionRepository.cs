using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionByUserId(int user_id);
        Task<Transaction> GetTransactionById(int id);

        Task<bool> CreateTransaction(Transaction transaction);
        Task<bool> UpdateTransaction(int id, Transaction transaction);
        Task<bool> DeleteTransaction(int id);

    }
}
