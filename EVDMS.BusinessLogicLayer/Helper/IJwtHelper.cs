namespace EVDMS.BusinessLogicLayer.Helper;

public interface IJwtHelper
{
    public (string, string, string) GenerateToken(string userName, string fullName, string roleName);
}