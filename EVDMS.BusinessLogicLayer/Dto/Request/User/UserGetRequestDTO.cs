namespace EVDMS.BusinessLogicLayer.Dto.Request.User;

public class UserGetRequestDTO : Paging
{
    public string userName { get; set; } = string.Empty;
    public string fullName { get; set; } = string.Empty;
}