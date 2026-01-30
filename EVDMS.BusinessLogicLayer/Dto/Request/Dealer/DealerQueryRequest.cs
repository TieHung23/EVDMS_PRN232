namespace EVDMS.BusinessLogicLayer.Dto.Request.Dealer;

public class DealerQueryRequest : Paging
{
    public string? Search { get; set; }
    public string? Sort { get; set; }
}
