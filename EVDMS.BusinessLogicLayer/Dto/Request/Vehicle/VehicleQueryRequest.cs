namespace EVDMS.BusinessLogicLayer.Dto.Request.Vehicle;

public class VehicleQueryRequest : Paging
{
    public string ModelName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public int? ReleaseYear { get; set; }
}
