namespace EVDMS.BusinessLogicLayer.Dto.Request.Dealer;

public class DealerGetFilter : Paging
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}