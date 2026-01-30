namespace EVDMS.BusinessLogicLayer.Dto.Response.Vehicle;

public class VehicleListResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int Count { get; set; }
    public IEnumerable<VehicleResponse> Items { get; set; } = Array.Empty<VehicleResponse>();
}
