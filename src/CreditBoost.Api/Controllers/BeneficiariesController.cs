using CreditBoost.Api.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditBoost.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BeneficiariesController(IBeneficiaryQuery beneficiaryQuery) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var beneficiaries = await beneficiaryQuery.GetAvailables();

        return Ok(beneficiaries);
    }
}
