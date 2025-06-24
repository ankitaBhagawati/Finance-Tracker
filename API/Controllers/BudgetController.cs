using Finance_Tracker.Interfaces;
using Finance_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace API.Controllers;
[Authorize]
public class BudgetController : ApiControllerBase
{
    private readonly IBudgetsService _budgetsService;

    public BudgetController(IBudgetsService budgetsService)
    {
        _budgetsService = budgetsService;
    }
    [HttpGet]
    [Route("GetAllBudget/{user_Id}")]
    public async Task<ActionResult<IEnumerable>> GetAllBudget(int user_Id)
    {
        var result= await _budgetsService.GetAllBudgetByUserId(user_Id);
        return Ok(result);
    }

    [HttpGet]
    [Route("GetBudgetById/{id}")]
    public async Task<ActionResult> GetBudgetById(int id)
    {
        var result = await _budgetsService.GetBudgetById(id);
        return Ok(result);
    }
    [HttpPost]
    [Route("CreateBudget/{budget}/")]
   public async Task<ActionResult> CreateOrUpdateBudget([FromBody] Budgets budget)
    {
        bool result = await _budgetsService.CreateBudget(budget);
        return Ok(result);
    }
    [HttpPost]
    [Route("UpdateBudget/{budget}")]
    public async Task<ActionResult> UpdateBudget([FromBody] Budgets budget)
    {
        bool result = await _budgetsService.UpdateBudget(budget); 
        return Ok(result);
    }

}
