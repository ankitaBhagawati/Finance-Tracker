using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class ExpenseController : ApiControllerBase
{
    [HttpGet]
    public IActionResult GetExpenses()
    {
        return Ok();
    }
}
