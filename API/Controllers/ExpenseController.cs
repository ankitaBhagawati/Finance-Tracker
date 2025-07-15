using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Repositories.Interfaces;
using System.Collections;

namespace API.Controllers;

[Authorize]
public class ExpenseController : ApiControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly IAuthService _authService;

    public ExpenseController(ITransactionService transactionService, IAuthService authService)
    {
        _transactionService= transactionService;
        _authService = authService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable>> GetAllExpenses()
    {
        var userId = _authService.GetUserID();
        var result = await _transactionService.GetAllTransactionByUserId(userId);
        return Ok(result);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetExpenseById(int id)
    {
        var result = await _transactionService.GetTransactionById(id);
        return Ok(result);
    }
    [HttpPost]
    [Route("")]
    public async Task<ActionResult> CreateExpense([FromBody] Transaction transaction)
    {
        bool result = await _transactionService.CreateTransaction(transaction);
        return Ok(result);
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateExpense(int id, [FromBody] Transaction transaction)
    {
        bool result = await _transactionService.UpdateTransaction(id, transaction);
        return Ok(result);
    }

    [HttpDelete]
    [Route("id")]
    public async Task<ActionResult> DeleteExpense(int id)
    {
        bool result = await _transactionService.DeleteTransaction(id);
        return Ok(result);
    }
}
