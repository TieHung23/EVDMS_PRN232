using System.Security.Claims;
using EVDMS.BusinessLogicLayer.Dto.Request;
using EVDMS.BusinessLogicLayer.Dto.Request.Config;
using EVDMS.BusinessLogicLayer.Service;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiResponse = EVDMS.BusinessLogicLayer.Dto.Response.Response;

namespace EVDMS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigsController : ControllerBase
{
    private readonly IConfigService _configService;
    private readonly  IHttpContextAccessor _httpContextAccessor;

    public ConfigsController(IConfigService configService, IHttpContextAccessor httpContextAccessor)
    {
        _configService = configService;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Get all configs with search, filtering, sorting, and paging
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ConfigQueryRequest request)
    {
        var result = await _configService.GetAllAsync(request);

        if (result.IsFailed)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get config by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _configService.GetByIdAsync(id);

        if (result.IsFailed)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Create a new config
    /// </summary>
    [HttpPost]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Create([FromBody] CreateConfigRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse.Failed("Validation failed."));
        }

        // TODO: Get actual user from JWT claims when authentication is implemented
        var createdBy = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;;

        var result = await _configService.CreateAsync(request, createdBy);

        if (result.IsFailed)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result);
    }

    /// <summary>
    /// Update an existing config
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateConfigRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ApiResponse.Failed("Validation failed."));
        }

        // TODO: Get actual user from JWT claims when authentication is implemented
        var modifiedBy = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;;

        var result = await _configService.UpdateAsync(id, request, modifiedBy);

        if (result.IsFailed)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Delete a config (soft delete)
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var modifiedBy =_httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;;

        var result = await _configService.DeleteAsync(id, modifiedBy);

        if (result.IsFailed)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
