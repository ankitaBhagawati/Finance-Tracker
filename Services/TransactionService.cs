using Microsoft.Extensions.Configuration;
using Models;
using Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService: ITransactionService
    {
        private readonly IConfiguration _configuration;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IConfiguration configuration, ITransactionRepository transactionRepository)
        {
            _configuration = configuration;
            _transactionRepository = transactionRepository;
        }
        public Task<IEnumerable<Transaction>> GetAllTransactionByUserId(int user_id)
        {
            var result = _transactionRepository.GetAllTransactionByUserId(user_id);
            return result;
        }
        public Task<Transaction> GetTransactionById(int id)
        {
            var result = _transactionRepository.GetTransactionById(id);
            return result;
        }
        public Task<bool> CreateTransaction(Transaction transaction)
        {
            var result = _transactionRepository.CreateTransaction(transaction);
            return result;
        }
        public Task<bool> UpdateTransaction(int id, Transaction transaction)
        {
            var result = _transactionRepository.UpdateTransaction(id, transaction);
            return result;
        }
        public Task<bool> DeleteTransaction(int id)
        {
            var result = _transactionRepository.DeleteTransaction(id);
            return result;
        }

    }
}
