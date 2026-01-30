using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.User;

public class UserUpdateDTO
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public Guid? RoleId { get; set; }
    public Guid? DealerId { get; set; }

    [JsonIgnore]
    public string ModifiedBy { get; set; } = string.Empty;
}