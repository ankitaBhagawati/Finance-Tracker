using Finance_Tracker.Models;

namespace Finance_Tracker.Interfaces
{
    public interface IBudgetsService
    {
        Task<IEnumerable<Budgets>> GetAllBudgetByUserId(int user_id);
        Task<Budgets> GetBudgetById(int id);

        Task<bool> CreateBudget(Budgets budget);
        Task<bool> UpdateBudget(int id, Budgets budget);
        Task<bool> DeleteBudget(int id);
    }
}
