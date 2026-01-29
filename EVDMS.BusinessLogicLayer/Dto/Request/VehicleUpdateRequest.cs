namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class VehicleUpdateRequest
{
    public string ModelName { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string VehicleType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string ModifiedBy { get; set; } = string.Empty;
}
