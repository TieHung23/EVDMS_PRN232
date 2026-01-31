using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] VehicleQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _vehicleService.GetListAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(new { message = "Invalid GUID format.", field = "id" });
        }

        var result = await _vehicleService.GetByIdAsync(guidId, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VehicleCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _vehicleService.CreateAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] VehicleUpdateRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(new { message = "Invalid GUID format.", field = "id" });
        }

        var result = await _vehicleService.UpdateAsync(guidId, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, [FromBody] VehicleDeleteRequest request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(id, out var guidId))
        {
            return BadRequest(new { message = "Invalid GUID format.", field = "id" });
        }

        var result = await _vehicleService.DeleteAsync(guidId, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return NoContent();
    }
}

