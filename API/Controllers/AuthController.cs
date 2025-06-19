using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models.DTOs;
using Services.Repositories.Interfaces;

namespace API.Controllers;

public class AuthController : ApiControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Signup()
    {
        var dto = new SignupDTO
        {
            Email = "ankitaa@gmail.com",
            Name = "pagal",
            Password = "nhi h"
        };
        var created = _authService.Signup(dto);

        if (!created)
        {
            return StatusCode(500, "You're not worthy");
        }
        return Ok();
    }
}