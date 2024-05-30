using CreditBoost.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
    {
        var result = await mediator.Send(request);

        if (result != ValidationResult.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(Login), "User created successfully!");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await mediator.Send(command);

        return result.IsAuthenticated ?
            Ok(result) :
            Unauthorized();
    }
}
