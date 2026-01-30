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
}
