using System.ComponentModel.DataAnnotations;

namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class CreateConfigRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid VehicleId { get; set; }
}

public class UpdateConfigRequest
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Guid VehicleId { get; set; }
}

public class ConfigQueryRequest : Paging
{
    public string? SearchTerm { get; set; }
    public Guid? VehicleId { get; set; }
    public bool? IsActive { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}
