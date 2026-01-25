using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Role;
using EVDMS.Core.Model;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IRoleService
{
    Task<TResponse<List<RoleResponse>>> GetAllRolesAsync();
    Task<TResponse<RoleResponse>> GetRoleByIdAsync(Guid id);
}