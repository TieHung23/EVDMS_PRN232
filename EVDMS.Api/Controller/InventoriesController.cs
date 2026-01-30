using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class InventoriesController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoriesController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
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
    public async Task<IActionResult> Create([FromBody] InventoryCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _inventoryService.CreateAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] InventoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _inventoryService.UpdateAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromBody] InventoryDeleteRequest request, CancellationToken cancellationToken)
    {
        var result = await _inventoryService.DeleteAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return NoContent();
    }
}
