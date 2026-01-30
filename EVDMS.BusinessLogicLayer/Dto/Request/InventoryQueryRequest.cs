namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class InventoryQueryRequest : Paging
{
    public Guid? DealerId { get; set; }
    public Guid? VehicleId { get; set; }
    public int? MinQuantity { get; set; }
}
