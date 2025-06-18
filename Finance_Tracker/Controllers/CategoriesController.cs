using Finance_Tracker.Interfaces;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finance_Tracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _CategoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _CategoriesService = categoriesService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Categories>>> GetAll()
        {
            var list = await _CategoriesService.GetAllCategories();
            return Ok(list);
        }
    }
}
