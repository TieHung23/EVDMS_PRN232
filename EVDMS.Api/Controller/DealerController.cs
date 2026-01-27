using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/dealers")]
public class DealerController : ControllerBase
{
    private readonly IDealerService _dealerService;

    public DealerController(IDealerService dealerService)
    {
        _dealerService = dealerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] DealerQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _dealerService.GetListAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _dealerService.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DealerCreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _dealerService.CreateAsync(request, cancellationToken);
        if (result.IsFailed) return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DealerUpdateRequest request, CancellationToken cancellationToken)
    {
        var result = await _dealerService.UpdateAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromBody] DealerDeleteRequest request, CancellationToken cancellationToken)
    {
        var result = await _dealerService.DeleteAsync(id, request, cancellationToken);
        if (result.IsFailed) return NotFound(result);

        return NoContent();
    }
}
