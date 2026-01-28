namespace EVDMS.BusinessLogicLayer.Dto.Request.User;

public class UserRequestLoginDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}