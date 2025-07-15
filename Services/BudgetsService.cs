using Microsoft.Extensions.Configuration;
using Models;
using Services.Repositories.Interfaces;

namespace Services
{
    public class BudgetsService : IBudgetsService
    {
        private readonly IBudgetsRepository _budgetRepository;
        private readonly IConfiguration _configuration;

        public BudgetsService(IBudgetsRepository budgetsRepository, IConfiguration configuration)
        {
            _budgetRepository = budgetsRepository;
            _configuration = configuration;
        }

        public Task<IEnumerable<Budgets>> GetAllBudgetByUserId(int user_id)
        {
            var result = _budgetRepository.GetAllBudgetByUserId(user_id);
            return result;
        }

        public Task<Budgets> GetBudgetById(int id)
        {
            var result = _budgetRepository.GetBudgetById(id);
            return result;
        }

        public Task<bool> CreateBudget(Budgets budget)
        {
            Task<bool> result = _budgetRepository.CreateBudget(budget);
            return result;
        }

        public Task<bool> UpdateBudget(int id, Budgets budget)
        {
            Task<bool> result = _budgetRepository.UpdateBudget(id, budget);
            return result;
        }

        public Task<bool> DeleteBudget(int id)
        {
            Task<bool> result = _budgetRepository.DeleteBudget(id);
            return result;
        }
    }
}
