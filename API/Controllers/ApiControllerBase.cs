using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[Controller]
[Route("/api/v1/[controller]")]
public abstract class ApiControllerBase : ControllerBase { }