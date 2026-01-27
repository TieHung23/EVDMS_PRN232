namespace EVDMS.BusinessLogicLayer.Dto.Request;

public class DealerUpdateRequest
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ModifiedBy { get; set; } = string.Empty;
}
