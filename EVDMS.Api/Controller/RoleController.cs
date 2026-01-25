using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.BusinessLogicLayer.Service.Implement;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var response = await _roleService.GetAllRolesAsync();
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoleById(Guid id)
    {
        var response = await _roleService.GetRoleByIdAsync(id);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}