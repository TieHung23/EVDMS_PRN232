namespace EVDMS.BusinessLogicLayer.Dto.Response.Vehicle;

public class VehicleResponse
{
    public Guid Id { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public static VehicleResponse FromEntity(Core.Model.Vehicle vehicle)
    {
        return new VehicleResponse
        {
            Id = vehicle.Id,
            ModelName = vehicle.ModelName,
            Brand = vehicle.Brand,
            VehicleType = vehicle.VehicleType,
            Description = vehicle.Description,
            ReleaseYear = vehicle.ReleaseYear,
            IsActive = vehicle.IsActive,
            IsDeleted = vehicle.IsDeleted,
            CreatedAt = vehicle.CreatedAt,
            ModifiedAt = vehicle.ModifiedAt
        };
    }
}
