using EVDMS.BusinessLogicLayer.Dto.Request.User;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.User;

namespace EVDMS.BusinessLogicLayer.Service.Abstraction;

public interface IUserService
{
    Task<TResponse<List<UserGetDTO>>>GetAllUsersAsync(UserGetRequestDTO request);
    Task<TResponse<UserGetDTO>> GetUserByIdAsync(Guid id);

    Task<Response> CreateUserAsync(UserCreateDTO request);

    Task<Response> UpdateUserAsync(Guid id, UserUpdateDTO request);

    Task<Response> DeleteUserAsync(Guid id, UserDeleteDTO request);
}