using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DealerController : ControllerBase
{
    private readonly IDealerService _dealerService;

    public DealerController(IDealerService dealerService)
    {
        _dealerService = dealerService;
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
    public async Task<IActionResult> GetAllDealers()
    {
        var response = await _dealerService.GetAllDealersAsync();
        if (!response.IsSuccess)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
}