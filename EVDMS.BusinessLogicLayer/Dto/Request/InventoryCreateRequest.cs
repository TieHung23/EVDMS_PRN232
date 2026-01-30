namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class InventoryCreateRequest
{
    public Guid DealerId { get; set; }
    public Guid VehicleId { get; set; }
    public int Quantity { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
