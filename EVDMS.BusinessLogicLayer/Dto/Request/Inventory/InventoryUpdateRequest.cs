namespace EVDMS.BusinessLogicLayer.Dto.Request.Inventory;

public class InventoryUpdateRequest
{
    public Guid DealerId { get; set; }
    public Guid VehicleId { get; set; }
    public int Quantity { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
}
