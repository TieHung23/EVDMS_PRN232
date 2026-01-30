using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Dealer;
using EVDMS.BusinessLogicLayer.Dto.Response.Role;
using EVDMS.BusinessLogicLayer.Dto.Response.User;
using EVDMS.Core.Model;

namespace EVDMS.BusinessLogicLayer.Helper;

public static class Mapper
{
    #region Role Mapper
    public static RoleDTO CreateRoleResponse(Role role)
    {
        return new RoleDTO
        {
            Name = role.Name,
            Description = role.Description,
            Id = role.Id
        };
    }

    public static List<RoleDTO> CreateRoleResponseList(List<Role> roles)
    {
        return roles.Select(CreateRoleResponse).ToList();
    }
    #endregion


    #region Dealer Mapper
    public static DealerResponse CreateDealerResponse(Dealer dealer)
    {
        return new DealerResponse
        {
            Id = dealer.Id,
            Name = dealer.Name,
            Code =  dealer.Code,
            Email =  dealer.Email,
            CreatedAt =  dealer.CreatedAt,
            ModifiedAt = dealer.ModifiedAt
        };
    }

    public static List<DealerResponse> CreateDealerResponseList(List<Dealer> dealers)
    {
        return dealers.Select(CreateDealerResponse).ToList();
    }


    #endregion

    #region User Mapper
    public static UserGetDTO GetUserDTO(User user)
    {
        var dealerDto = new DealerWithUserDTO(user.Dealer!.Id, user.Dealer.Code, user.Dealer.Name);
        var roleDto = new RoleWithUserDTO(user.Role!.Id, user.Role.Name);

        var userDto = new UserGetDTO(userId: user.Id, userName: user.UserName, fullName: user.FullName, dealerDetail: dealerDto, roleDetail: roleDto);
        return userDto;
    }
    public static List<UserGetDTO> GetUserDTOList(List<User> users)
    {
        return users.Select(GetUserDTO).ToList();
    }

    #endregion
}
