using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.User;

public class UserCreateDTO
{
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid RoleId { get; set; } = Guid.Empty;
    public Guid DealerId { get; set; } = Guid.Empty;

    [JsonIgnore]
    public string CreatedBy { get; set; } = string.Empty;
}