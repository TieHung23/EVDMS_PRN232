using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;

public class VehicleCreateRequest
{
    public string ModelName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    [JsonIgnore]
    public string CreatedBy { get; set; } = string.Empty;
}
