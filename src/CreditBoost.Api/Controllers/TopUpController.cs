using CreditBoost.Api.Application.Commands;
using CreditBoost.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopUpController(IMediator mediator, ITopUpOptionQuery topUpOptionQuery) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> TopUpTransaction([FromBody] CreateTopUpTransactionCommand request)
    {
        var result = await mediator.Send(request);

        if (result != ValidationResult.Success)
        {
            return BadRequest(result);
        }

        return Created();
    }

    [HttpGet("options")]
    public async Task<IActionResult> GetOptions()
    {
        var topUpOptions = await topUpOptionQuery.GetAvailables();
        return Ok(topUpOptions);
    }
}
