using CreditTopUp.Application.Commands;
using CreditTopUp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CreditTopUp.Api.Controllers;

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

        return CreatedAtAction(nameof(GetById), new { request.Id }, "Top up transaction created successfully!");
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
