using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    [Route("SignUp")]
    public IActionResult Signup([FromBody] SignupDTO dto)
    {
        var created = _authService.Signup(dto);

        if (!created)
        {
            return StatusCode(500, "You're not worthy");
        }
        return Ok();
    }

    [HttpPost]
    [Route("SignIn")]
    public IActionResult SignIn([FromBody] SignInDTO dto)
    {
        var token = _authService.SignIn(dto);
        if (token == null)
        {
            return StatusCode(401, "You're not allowed");
        }

        Response.Headers.Append("Authorization", "Bearer " + token);
        return Ok();
    }
}
