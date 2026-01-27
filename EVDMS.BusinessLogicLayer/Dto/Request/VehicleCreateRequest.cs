namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class VehicleCreateRequest
{
    public string ModelName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
