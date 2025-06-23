using Finance_Tracker.Interfaces;
using Finance_Tracker.Models;
using Services.Repositories.Interfaces;

namespace Services.Repositories
{
    public class BudgetsRepository(IDbService dbService) : IBudgetsRepository
    {
        private readonly IDbService _dbService = dbService;

        //DB operations not completed
        //Will do once tables/SP  are created
        public Task<IEnumerable<Budgets>> GetAllBudgetByUserId(int user_id)
        {

            throw new NotImplementedException();
        }

        public Task<Budgets> GetBudgetById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> CreateBudget(Budgets budget)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBudget(Budgets budget) 
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteBudget(int id) 
        {
            throw new NotImplementedException();
        }
    }
}
