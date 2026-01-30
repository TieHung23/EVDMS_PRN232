using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class DealerCreateRequest
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    [JsonIgnore]
    public string CreatedBy { get; set; } = string.Empty;
}
