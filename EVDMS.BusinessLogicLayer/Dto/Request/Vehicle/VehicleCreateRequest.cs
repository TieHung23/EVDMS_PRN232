using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;

public class VehicleCreateRequest
{
    [Required]
    public required string ModelName { get; set; } = string.Empty;
    [Required]
    public required string Brand { get; set; } = string.Empty;
    [Required]
    public required string VehicleType { get; set; } = string.Empty;
    [Required]
    public required string Description { get; set; } = string.Empty;
    [Range(1900, 2100)]
    public required int ReleaseYear { get; set; }
    [JsonIgnore]
    public string CreatedBy { get; set; } = string.Empty;
}
