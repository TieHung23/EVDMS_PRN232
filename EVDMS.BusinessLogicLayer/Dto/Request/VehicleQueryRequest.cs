namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class VehicleQueryRequest : Paging
{
    public string? Search { get; set; }
    public string? Sort { get; set; }
}
