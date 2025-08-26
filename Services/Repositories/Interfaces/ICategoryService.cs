using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repositories.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategories();
        public Task<bool> CreateCategory(Category category);
        public Task<bool> UpdateCategory(Category category);
        public Task<bool> DeleteCategory(int id);
    }
}
