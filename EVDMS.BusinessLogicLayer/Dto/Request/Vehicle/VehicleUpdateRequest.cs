using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;

public class VehicleUpdateRequest
{
    public string ModelName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    [JsonIgnore]
    public string ModifiedBy { get; set; } = string.Empty;
}
