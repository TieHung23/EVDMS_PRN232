namespace EVDMS.BusinessLogicLayer.Dto.Response.Inventory;

public class InventoryListResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int Count { get; set; }
    public IEnumerable<InventoryResponse> Items { get; set; } = Array.Empty<InventoryResponse>();
}
