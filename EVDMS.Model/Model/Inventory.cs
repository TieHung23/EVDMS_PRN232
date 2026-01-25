using EVDMS.Core.Base;

namespace EVDMS.Core.Model;

public class Inventory : TIdentity<Guid>, ICreatable, IModifiable, IStatus
{
    public Inventory() : base(Guid.NewGuid())
    {
    }


    public Guid DealerId { get; set; }
    public Guid VehicleId { get; set; }

    public Vehicle? Vehicle { get; set; }

    public Dealer? Dealer { get; set; }

    public int Quantity { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string CreatedAtTick { get; set; }
    public required string CreatedBy { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public required string ModifiedAtTick { get; set; }
    public required string ModifiedBy { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsDeleted { get; set; }
}