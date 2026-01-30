using System.Linq.Expressions;
using EVDMS.BusinessLogicLayer.Dto.Request.User;
using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.User;
using EVDMS.BusinessLogicLayer.Helper;
using EVDMS.BusinessLogicLayer.Service.Abstraction;
using EVDMS.Core.Model;
using EVDMS.DataAccessLayer.Const;
using EVDMS.DataAccessLayer.Repository.Abstraction;

namespace EVDMS.BusinessLogicLayer.Service.Implement;

public class UserService : IUserService
{
    private  readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse<List<UserGetDTO>>> GetAllUsersAsync(UserGetRequestDTO request)
    {
        Expression<Func<User, bool> > filterOps = user => user.UserName.Contains(request.userName) &&
                                                        user.FullName.Contains(request.fullName);
        var includeProperties = $"{Const.NameOfClasses.Role},{Const.NameOfClasses.Dealer}";
        var paging = RequestExtensions.ToSkipTake(request.PageNumber, request.PageSize);



        var users = await _unitOfWork.GetRepository<User, Guid>().GetFilterAsync(
            filter: filterOps,
            orderBy: null,
            includeProperties: includeProperties,
            disableTracking: true,
            skip: paging.skip,
            take: paging.take
            );

        var responseData = Mapper.GetUserDTOList(users.ToList());
        return TResponse<List<UserGetDTO>>.Success(responseData, Const.GetSuccessMessage(Const.NameOfClasses.User));
    }

    public async Task<TResponse<UserGetDTO>> GetUserByIdAsync(Guid id)
    {
        Expression<Func<User, bool>> filter = user => user.Id == id;
        var includeProperties = $"{Const.NameOfClasses.Role},{Const.NameOfClasses.Dealer}";
        var user = await _unitOfWork.GetRepository<User, Guid>().GetFirstOrDefaultAsync(
            filter: filter,
            includeProperties: includeProperties,
            disableTracking: true);
        if (user is null)
        {
            return TResponse<UserGetDTO>.Failed(Const.GetNotFoundMessage(Const.NameOfClasses.User));
        }
        var responseData = Mapper.GetUserDTO(user);
        return TResponse<UserGetDTO>.Success(responseData, Const.GetSuccessMessage(Const.NameOfClasses.User));
    }

    public Task<Response> CreateUserAsync(UserCreateDTO request)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            FullName = request.FullName,
            HashedPassword = HashingPassword.HashPassword(request.Password),
            RoleId = request.RoleId,
            DealerId = request.DealerId,
            IsActive = true,
            IsDeleted = false,
            CreatedAt = DateTime.Now,
            CreatedAtTick = DateTime.Now.Ticks.ToString(),
            CreatedBy = request.CreatedBy,
            ModifiedAt = DateTime.Now,
            ModifiedAtTick = DateTime.Now.Ticks.ToString(),
            ModifiedBy = request.CreatedBy
        };

        _unitOfWork.GetRepository<User, Guid>().AddAsync(user);
        _unitOfWork.SaveChangesAsync();
        return Task.FromResult(Response.Success(Const.GetCreateSuccessMessage(Const.NameOfClasses.User)));
    }

    public async Task<Response> UpdateUserAsync(Guid id, UserUpdateDTO request)
    {
        var user = await _unitOfWork.GetRepository<User, Guid>().GetByIdAsync(id);
        if (user is null)
        {
            return Response.Failed(Const.GetNotFoundMessage(Const.NameOfClasses.User));
        }
        user.UserName = request.UserName ?? user.UserName;
        user.FullName = request.FullName ?? user.FullName;
        if (!string.IsNullOrEmpty(request.Password))
        {
            user.HashedPassword = HashingPassword.HashPassword(request.Password);
        }

        user.RoleId = request.RoleId ?? user.RoleId;
        user.DealerId = request.DealerId ?? user.DealerId;
        user.ModifiedAt = DateTime.Now;
        user.ModifiedAtTick = DateTime.Now.Ticks.ToString();
        user.ModifiedBy = request.ModifiedBy;

        _unitOfWork.GetRepository<User, Guid>().Update(user);
        await _unitOfWork.SaveChangesAsync();
        return Response.Success(Const.GetUpdateSuccessMessage(Const.NameOfClasses.User));
    }

    public Task<Response> DeleteUserAsync(Guid id, UserDeleteDTO request)
    {
        var user =  _unitOfWork.GetRepository<User, Guid>().GetByIdAsync(id).Result;
        if (user is null)
        {
            return Task.FromResult(Response.Failed(Const.GetNotFoundMessage(Const.NameOfClasses.User)));
        }
        user.IsDeleted = true;
        user.ModifiedAt = DateTime.Now;
        user.ModifiedAtTick = DateTime.Now.Ticks.ToString();
        user.ModifiedBy = request.ModifiedBy;

        _unitOfWork.GetRepository<User, Guid>().Update(user);
        _unitOfWork.SaveChangesAsync();
        return Task.FromResult(Response.Success(Const.GetDeleteSuccessMessage(Const.NameOfClasses.User)));
    }
}