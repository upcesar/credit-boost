using CreditBoost.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditBoost.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopUpController(IMediator mediator, ITopUpOptionQuery topUpOptionQuery) : ControllerBase
{
    [HttpGet("options")]
    public async Task<IActionResult> GetOptions()
    {
        var topUpOptions = await topUpOptionQuery.GetAvailable();
        return Ok(topUpOptions);
    }
}
