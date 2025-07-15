using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface IBudgetsRepository
    {
        Task<IEnumerable<Budgets>> GetAllBudgetByUserId(int user_id);
        Task<Budgets> GetBudgetById(int id);

        Task<bool> CreateBudget(Budgets budget);
        Task<bool> UpdateBudget(int id, Budgets budget);
        Task<bool> DeleteBudget(int id);
    }
}
