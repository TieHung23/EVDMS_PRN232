using System.Security.Claims;
using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly IInventoryService _inventoryService;
    private readonly  IHttpContextAccessor _httpContextAccessor;

    public InventoriesController(IInventoryService inventoryService, IHttpContextAccessor httpContextAccessor)
    {
        _inventoryService = inventoryService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] InventoryQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _inventoryService.GetListAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _inventoryService.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Create([FromBody] InventoryCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.CreatedBy = userId!;

        var result = await _inventoryService.CreateAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Update(Guid id, [FromBody] InventoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.ModifiedBy = userId!;

        var result = await _inventoryService.UpdateAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var request = new InventoryDeleteRequest();

        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.ModifiedBy = userId!;

        var result = await _inventoryService.DeleteAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return NoContent();
    }
}
