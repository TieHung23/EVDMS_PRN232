using EVDMS.BusinessLogicLayer.Dto.Response;
using EVDMS.BusinessLogicLayer.Dto.Response.Role;
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
}
