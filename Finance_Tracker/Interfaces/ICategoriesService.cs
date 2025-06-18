using Finance_Tracker.Models;

namespace Finance_Tracker.Interfaces
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Categories>> GetAllCategories();
        Task<Categories> GetCategoryById(int id);
    }
}
