using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;

public class VehicleDeleteRequest
{
    [JsonIgnore]
    public string ModifiedBy { get; set; } = string.Empty;
}
