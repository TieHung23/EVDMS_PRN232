namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class DealerQueryRequest : Paging
{
    public string? Search { get; set; }
    public string? Sort { get; set; }
}
