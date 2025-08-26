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
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;


        public CategoryService(ICategoryRepository categoryRepository, IConfiguration configuration)
        {
            _categoryRepository = categoryRepository;
            _configuration = configuration;
        }

        public Task<bool> CreateCategory(Category category)
        {
            Task<bool> result =  _categoryRepository.CreateCategory(category);
            return result;
        }

        public Task<bool> DeleteCategory(int id)
        {
            Task<bool> result = _categoryRepository.DeleteCategory(id);
            return result;
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            var result = _categoryRepository.GetCategories();
            return result;
        }

        public Task<bool> UpdateCategory(Category category)
        {
            Task<bool> result = _categoryRepository.UpdateCategory(category);
            return result;
        }
    }
}
