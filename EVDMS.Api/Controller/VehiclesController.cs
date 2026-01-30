using System.Security.Claims;
using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly  IHttpContextAccessor _httpContextAccessor;

    public VehiclesController(IVehicleService vehicleService, IHttpContextAccessor httpContextAccessor)
    {
        _vehicleService = vehicleService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] VehicleQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _vehicleService.GetListAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _vehicleService.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Create([FromBody] VehicleCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.CreatedBy = userId!;
        var result = await _vehicleService.CreateAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VehicleUpdateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.ModifiedBy = userId!;

        var result = await _vehicleService.UpdateAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Delete(Guid id, [FromBody] VehicleDeleteRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.ModifiedBy = userId!;

        var result = await _vehicleService.DeleteAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return NoContent();
    }
}
