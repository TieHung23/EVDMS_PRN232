using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Role;
using EVDMS.BusinessLogicLayer.Helper;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Const;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse<RoleResponse>> GetAllRolesAsync()
    {
        var roles =await  _unitOfWork.GetRepository<Role, Guid>().GetAllAsync();

        var roleDtos = Mapper.CreateRoleResponseList(roles.ToList());

        var roleResponses = new RoleResponse
        {
            Roles = roleDtos
        };

        return TResponse<RoleResponse>.Success(roleResponses, Const.GetSuccessMessage(Const.NameOfClasses.Role));
    }

    public async Task<TResponse<RoleResponse>> GetRoleByIdAsync(Guid id)
    {
        var role = await _unitOfWork.GetRepository<Role, Guid>().GetByIdAsync(id);

        if (role == null)
        {
            return TResponse<RoleResponse>.Failed("Role not found");
        }

        var roleDto = Mapper.CreateRoleResponse(role);

        var roleResponse = new RoleResponse
        {
            Roles = [roleDto],
        };

        return TResponse<RoleResponse>.Success(roleResponse, Const.GetSuccessMessage(Const.NameOfClasses.Role));
    }
}