using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class DealerDeleteRequest
{
    [JsonIgnore]
    public string ModifiedBy { get; set; } = string.Empty;
}
