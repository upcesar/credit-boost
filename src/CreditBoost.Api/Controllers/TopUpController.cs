using CreditBoost.Api.Application.Commands;
using CreditBoost.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CreditBoost.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TopUpController(
    IMediator mediator,
    ITopUpOptionQuery topUpOptionQuery,
    ITopUpTransactionQuery topUpTransactionQuery) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> TopUpTransaction([FromBody] CreateTopUpTransactionCommand request)
    {
        var result = await mediator.Send(request);

        if (result != ValidationResult.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetById), new { request.Id }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await topUpTransactionQuery.GetById(id);
        return transaction is null ? NotFound() : Ok(transaction);
    }

    [AllowAnonymous]
    [HttpGet("options")]
    public async Task<IActionResult> GetOptions()
    {
        var topUpOptions = await topUpOptionQuery.GetAvailables();
        return Ok(topUpOptions);
    }
}
