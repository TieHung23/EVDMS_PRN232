using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;

public class VehicleUpdateRequest
{
    [Required]
    public string ModelName { get; set; } = string.Empty;
    [Required]
    public string Brand { get; set; } = string.Empty;
    [Required]
    public string VehicleType { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Range(1900, 2100)]
    public int ReleaseYear { get; set; }
    [JsonIgnore]
    public string ModifiedBy { get; set; } = string.Empty;
}
