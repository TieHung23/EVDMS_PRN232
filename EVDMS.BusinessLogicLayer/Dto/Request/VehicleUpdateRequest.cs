using System.ComponentModel.DataAnnotations;

namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class VehicleUpdateRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ModelName is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "ModelName must be between 1 and 100 characters")]
    public string? ModelName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Brand is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Brand must be between 1 and 50 characters")]
    public string? Brand { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "VehicleType is required")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "VehicleType must be between 1 and 50 characters")]
    public string? VehicleType { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "ReleaseYear is required")]
    [Range(1900, 2100, ErrorMessage = "ReleaseYear must be between 1900 and 2100")]
    public int ReleaseYear { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "ModifiedBy is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "ModifiedBy must be between 1 and 100 characters")]
    public string? ModifiedBy { get; set; }
}
