using EVDMS.BusinessLogicLayer.Dto.Request;

namespace EVDMS.BusinessLogicLayer.Dto.Response.Role;

public class RoleDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class RoleResponse : Paging
{
    public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
}