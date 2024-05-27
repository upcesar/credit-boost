using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditBoost.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BeneficiariesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {


        return Ok();
    }
}
