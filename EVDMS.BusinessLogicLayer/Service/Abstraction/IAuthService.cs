using EVDMS.BusinessLogicLayer.Dto.Request.User;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.User;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IAuthService
{
    public Task<TResponse<UserResponseLoginDTO>> LoginAsync(UserRequestLoginDTO dto);
}