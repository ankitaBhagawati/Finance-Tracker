using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Repositories.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace API.Controllers;

//[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public class CategoryController : ApiControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IAuthService _authService;

    public CategoryController(ICategoryService categoryService, IAuthService authService)
    {
        _categoryService = categoryService;
        _authService = authService;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable>> GetAllCategory()
    {
        var result = await _categoryService.GetCategories();
        return Ok(result);
    }
    [HttpPost]
    [Route("")]
    public async Task<ActionResult<bool>> CreateCategory([FromBody] Category category)
    {
        var result = await _categoryService.CreateCategory(category);
        return Ok(result);
    }

    [HttpPut]
    [Route("")]
    public async Task<ActionResult<bool>> UpdateCategory([FromBody] Category category)
    {
        var result = await _categoryService.UpdateCategory(category);
        return Ok(result);
    }
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult<bool>> DeleteCategory(int id)
    {
        var result = await _categoryService.DeleteCategory(id);
        return Ok(result);
    }
}


