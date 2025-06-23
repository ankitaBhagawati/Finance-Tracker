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
    [Route("CreateOrUpdateBudget/{budget}/{isCreated}")]
   public async Task<ActionResult> CreateOrUpdateBudget([FromBody] Budgets budget, [FromQuery] bool isCreated)
    {
        bool result;
        if (isCreated)
        {
            result =  await _budgetsService.CreateBudget(budget);
        }
        else
        {
            result = await _budgetsService.UpdateBudget(budget);
        }
        return Ok(result);
    }

}
