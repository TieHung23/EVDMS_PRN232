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

    /// <summary>
    /// Convert to dictionary with selected fields only
    /// </summary>
    public Dictionary<string, object?> ToSelectedFields(IEnumerable<string>? fields)
    {
        var allFields = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase)
        {
            { "id", Id },
            { "name", Name },
            { "description", Description },
            { "vehicleId", VehicleId },
            { "vehicleName", VehicleName },
            { "createdAt", CreatedAt },
            { "createdBy", CreatedBy },
            { "modifiedAt", ModifiedAt },
            { "modifiedBy", ModifiedBy },
            { "isActive", IsActive }
        };

        if (fields == null || !fields.Any())
        {
            return allFields;
        }

        var selectedFields = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        foreach (var field in fields)
        {
            if (allFields.TryGetValue(field.Trim(), out var value))
            {
                selectedFields[field.Trim().ToLower()] = value;
            }
        }

        return selectedFields.Count > 0 ? selectedFields : allFields;
    }
}

public class ConfigListResponse
{
    public List<Dictionary<string, object?>> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}
