namespace EVDMS.BusinessLogicLayer.Dto.Response;

public class ConfigResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid VehicleId { get; set; }
    public string? VehicleName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime ModifiedAt { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class ConfigListResponse
{
    public List<ConfigResponse> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
