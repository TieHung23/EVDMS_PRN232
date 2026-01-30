using EVDMS.BusinessLogicLayer.Dto.Request.User;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.User;
using EVDMS.BusinessLogicLayer.Helper;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Const;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHelper _jwtHelper;

    public AuthService(IUnitOfWork unitOfWork, IJwtHelper jwtHelper)
    {
        _unitOfWork = unitOfWork;
        _jwtHelper = jwtHelper;
    }

    public async Task<TResponse<UserResponseLoginDTO>> LoginAsync(UserRequestLoginDTO dto)
    {
        var user = await _unitOfWork.GetRepository<User, Guid>().GetFirstOrDefaultAsync(
            filter: u => u.UserName == dto.Username,
            disableTracking: true,
            includeProperties: $"{Const.NameOfClasses.Role}"
        );

        var hashingPassword = HashingPassword.HashPassword(dto.Password);

        if (user == null)
        {
            return TResponse<UserResponseLoginDTO>.Failed("Invalid username or password.");
        }

        var isValidPassword = HashingPassword.VerifyPassword(
            dto.Password,
            user.HashedPassword
        );

        if (!isValidPassword)
        {
            return TResponse<UserResponseLoginDTO>.Failed("Invalid username or password.");
        }


        if (!user.IsActive || user.IsDeleted)
        {
            return TResponse<UserResponseLoginDTO>.Failed("User account is inactive.");
        }

        var token = _jwtHelper.GenerateToken(user.UserName, user.FullName, user.Role!.Name, user.Id.ToString());

        var response = TResponse<UserResponseLoginDTO>.Success(data: new UserResponseLoginDTO
        {
            AccessToken = token.Item2,
            RefreshToken = token.Item3
        }, message: "Login successful.");
        return response;
    }
}