namespace EVDMS.BusinessLogicLayer.Dto.Response.User;

public class UserResponseLoginDTO
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}