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

    public async Task<TResponse<List<RoleResponse>>> GetAllRolesAsync()
    {
        var roles =await  _unitOfWork.GetRepository<Role, Guid>().GetAllAsync();

        var roleResponses = Mapper.CreateRoleResponseList(roles.ToList());

        return TResponse<List<RoleResponse>>.Success(roleResponses, Const.GetSuccessMessage(Const.NameOfClasses.Role));
    }

    public async Task<TResponse<RoleResponse>> GetRoleByIdAsync(Guid id)
    {
        var role = await _unitOfWork.GetRepository<Role, Guid>().GetByIdAsync(id);

        if (role == null)
        {
            return TResponse<RoleResponse>.Failed("Role not found");
        }

        var roleResponse = Mapper.CreateRoleResponse(role);

        return TResponse<RoleResponse>.Success(roleResponse, Const.GetSuccessMessage(Const.NameOfClasses.Role));
    }
}