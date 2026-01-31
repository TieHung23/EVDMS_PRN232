using System.ComponentModel.DataAnnotations;

namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class VehicleDeleteRequest
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ModifiedBy is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "ModifiedBy must be between 1 and 100 characters")]
    public string? ModifiedBy { get; set; }
}
