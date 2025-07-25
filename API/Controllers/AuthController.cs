using Microsoft.AspNetCore.Authorization;
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
        var result = _authService.Signup(dto);

        if (!result.Success)
        {
            if (result.Code == 409)
                return BadRequest();

            return StatusCode(500);
        }

        return Created();
    }

    [HttpPost]
    [Route("SignIn")]
    public IActionResult SignIn([FromBody] SignInDTO dto)
    {
        var token = _authService.SignIn(dto);
        if (token == null)
        {
            return StatusCode(401);
        }

        Response.Headers.Append("Authorization", "Bearer " + token);
        return Created();
    }

    [Authorize]
    [HttpGet]
    [Route("me")]
    public IActionResult GetCurrentUser()
    {
        var user = _authService.GetUser();
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
