namespace EVDMS.BusinessLogicLayer.Dto.Response.Dealer;

public class DealerListResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int Count { get; set; }
    public IEnumerable<DealerResponse> Items { get; set; } = Array.Empty<DealerResponse>();
}
