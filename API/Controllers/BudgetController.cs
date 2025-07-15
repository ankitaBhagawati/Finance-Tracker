using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Repositories.Interfaces;
using System.Collections;

namespace API.Controllers;

[Authorize]
public class BudgetController : ApiControllerBase
{
    private readonly IBudgetsService _budgetsService;
    private readonly IAuthService _authService;

    public BudgetController(IBudgetsService budgetsService, IAuthService authService)
    {
        _authService = authService;
        _budgetsService = budgetsService;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<IEnumerable>> GetAllBudget()
    {
        var userId = _authService.GetUserID();
        var result = await _budgetsService.GetAllBudgetByUserId(userId);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetBudgetById(int id)
    {
        var result = await _budgetsService.GetBudgetById(id);
        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreateBudget([FromBody] Budgets budget)
    {
        bool result = await _budgetsService.CreateBudget(budget);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateBudget(int id, [FromBody] Budgets budget)
    {
        bool result = await _budgetsService.UpdateBudget(id, budget);
        return Ok(result);
    }
    [HttpDelete]
    [Route("id")]
    public async Task<ActionResult> DeleteBudget(int id)
    {
        bool result= await _budgetsService.DeleteBudget(id);
        return Ok(result);
    }
}
