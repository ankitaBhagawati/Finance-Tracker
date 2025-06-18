using Finance_Tracker.Interfaces;
using Finance_Tracker.Models;
using Finance_Tracker.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance_Tracker.Services
{
    public class CategoriesService: ICategoriesService
    {
        private readonly ICategoriesRepository _Categoriesrepository;

        public CategoriesService(ICategoriesRepository repository)
        {
            _Categoriesrepository = (CategoriesRepository?)repository;
        }
        public async Task<IEnumerable<Categories>> GetAllCategories()
        {
            return await _Categoriesrepository.GetAllCategories();
        }
        public async Task<Categories> GetCategoryById(int id)
        {
            return await _Categoriesrepository.GetCategoryById(id);
        }
    }
}
