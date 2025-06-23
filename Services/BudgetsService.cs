using Finance_Tracker.Interfaces;
using Finance_Tracker.Models;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class BudgetsService: IBudgetsService
    {
        private readonly IBudgetsRepository _budgetRepository;
        private readonly IConfiguration _configuration;

        public BudgetsService(IBudgetsRepository budgetsRepository, IConfiguration configuration)
        {
            _budgetRepository = budgetsRepository;
            _configuration= configuration;
        }
        public Task<IEnumerable<Budgets>> GetAllBudgetByUserId(int user_id)
        {
            var result= _budgetRepository.GetAllBudgetByUserId(user_id);
            return result;
        }
        public Task<Budgets> GetBudgetById(int id)
        {
            var result = _budgetRepository.GetBudgetById(id);
            return result;
        }
        public Task<bool> CreateBudget(Budgets budget)
        {
            Task<bool> result= _budgetRepository.CreateBudget(budget);
            return result;
        }
        public Task<bool> UpdateBudget(Budgets budget)
        {
            Task<bool> result= _budgetRepository.UpdateBudget(budget);
            return result;
        }
        public Task<bool> DeleteBudget(int id)
        {
            Task<bool> result=_budgetRepository.DeleteBudget(id);
            return result;
        }
    }
}
