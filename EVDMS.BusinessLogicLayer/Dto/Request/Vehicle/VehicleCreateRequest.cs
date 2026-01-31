using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;

public class VehicleCreateRequest
{
    public required string ModelName { get; set; } = string.Empty;
    public required string Brand { get; set; } = string.Empty;
    public required string VehicleType { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required int ReleaseYear { get; set; }
    [JsonIgnore]
    public string CreatedBy { get; set; } = string.Empty;
}
