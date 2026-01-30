using System.Security.Claims;
using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Dealer;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DealersController : ControllerBase
{
    private readonly IDealerService _dealerService;
    private readonly  IHttpContextAccessor _httpContextAccessor;

    public DealersController(IDealerService dealerService, IHttpContextAccessor httpContextAccessor)
    {
        _dealerService = dealerService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpPost]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Create([FromBody] DealerCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.CreatedBy = userId!;


        var result = await _dealerService.CreateAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return CreatedAtAction(nameof(GetDealerById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DealerUpdateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.ModifiedBy = userId!;

        var result = await _dealerService.UpdateAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var request = new DealerDeleteRequest();
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.ModifiedBy = userId!;


        var result = await _dealerService.DeleteAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDealerById(Guid id)
    {
        var response = await _dealerService.GetDealerByIdAsync(id);
        if (!response.IsSuccess)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDealers([FromQuery] DealerGetFilter filter)
    {
        var response = await _dealerService.GetAllDealersAsync(filter);
        return Ok(response);
    }
}