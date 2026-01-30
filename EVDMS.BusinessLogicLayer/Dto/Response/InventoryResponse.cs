using EVDMS.Core.Model;

namespace EVDMS.BusinessLogicLayer.Dto.Response;

public class InventoryResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    
    // Dealer info (flattened)
    public Guid DealerId { get; set; }
    public string DealerCode { get; set; } = string.Empty;
    public string DealerName { get; set; } = string.Empty;
    
    // Vehicle info (flattened)
    public Guid VehicleId { get; set; }
    public string VehicleBrand { get; set; } = string.Empty;
    public string VehicleModelName { get; set; } = string.Empty;
    
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    public static InventoryResponse FromEntity(Inventory inventory)
    {
        return new InventoryResponse
        {
            Id = inventory.Id,
            Quantity = inventory.Quantity,
            DealerId = inventory.DealerId,
            DealerCode = inventory.Dealer?.Code ?? string.Empty,
            DealerName = inventory.Dealer?.Name ?? string.Empty,
            VehicleId = inventory.VehicleId,
            VehicleBrand = inventory.Vehicle?.Brand ?? string.Empty,
            VehicleModelName = inventory.Vehicle?.ModelName ?? string.Empty,
            IsActive = inventory.IsActive,
            CreatedAt = inventory.CreatedAt,
            ModifiedAt = inventory.ModifiedAt
        };
    }
}
