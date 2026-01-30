using System.Security.Claims;
using EVDMS.BusinessLogicLayer.Dto.Request.User;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVDMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly  IHttpContextAccessor _httpContextAccessor;

    public UsersController(ILogger<UsersController> logger, IUserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] UserGetRequestDTO request)
    {
        var result = await _userService.GetAllUsersAsync(request);
        if (!result.IsSuccess)
        {
            NotFound(result);
        }
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        if (!result.IsSuccess)
        {
            NotFound(result);
        }
        return Ok(result);
    }

    [HttpPost]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Create([FromBody] UserCreateDTO request)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.CreatedBy = userId!;

        var result = await _userService.CreateUserAsync(request);
        if (!result.IsSuccess)
        {
            BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDTO request)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        request.ModifiedBy = userId!;

        var result = await _userService.UpdateUserAsync(id, request);
        if (!result.IsSuccess)
        {
            BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize("EVMStaffPolicy")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value;

        var request = new UserDeleteDTO(userId!);

        var result = await _userService.DeleteUserAsync(id, request);
        if (!result.IsSuccess)
        {
            BadRequest(result);
        }
        return Ok(result);
    }
}