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
public class BeneficiariesController(IMediator mediator, IBeneficiaryQuery beneficiaryQuery) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var beneficiaries = await beneficiaryQuery.GetAvailables();
        return Ok(beneficiaries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBeneficiaryCommand request)
    {
        var result = await mediator.Send(request);

        if (result != ValidationResult.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetById), new { request.Id }, "Beneficiary created successfully!");
    }
}
