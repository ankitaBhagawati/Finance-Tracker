using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Repositories.Interfaces;
using System.Collections;

namespace API.Controllers;

//[Authorize]
public class ExpenseController : ApiControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly ICategoryService _categoryService;
    private readonly IAuthService _authService;

    public ExpenseController(ITransactionService transactionService, ICategoryService categoryService, IAuthService authService)
    {
        _transactionService= transactionService;
        _categoryService = categoryService;
        _authService = authService;
    }
    #region Expense
    [HttpGet]
    public async Task<ActionResult<IEnumerable>> GetAllExpenses(int userId)
    {
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var result = await _transactionService.DeleteTransaction(id);
        return Ok(result);
    }

    #endregion


}
