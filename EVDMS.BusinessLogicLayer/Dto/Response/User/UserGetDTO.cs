namespace EVDMS.BusinessLogicLayer.Dto.Response.User;

public record UserGetDTO(Guid userId, string userName, string fullName, DealerWithUserDTO dealerDetail, RoleWithUserDTO roleDetail);

public record DealerWithUserDTO(Guid dealerId, string dealerCode, string dealerName);

public record RoleWithUserDTO(Guid roleId, string roleName);