using EVDMS.Core.Base;

namespace EVDMS.Core.Model;

public class Vehicle : TIdentity<Guid>, ICreatable, IModifiable, IStatus
{
    public Vehicle()
    {

    }

    public Vehicle(string modelName, string brand, string vehicleType, string description, int releaseYear) : base(Guid.NewGuid())
    {
        ModelName = modelName;
        Brand = brand;
        VehicleType = vehicleType;
        Description = description;
        ReleaseYear = releaseYear;
    }

    public string ModelName { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string VehicleType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string CreatedAtTick { get; set; }
    public required string CreatedBy { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required string ModifiedAtTick { get; set; }
    public required string ModifiedBy { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}