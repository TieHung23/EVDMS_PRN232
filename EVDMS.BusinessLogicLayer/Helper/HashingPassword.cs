namespace EVDMS.BusinessLogicLayer.Helper;

public static class HashingPassword
{
    public static string HashPassword(string password)
    {
        var result = BCrypt.Net.BCrypt.HashPassword(password);
        return result;
    }

    public static bool VerifyPassword(string providedPassword, string hashedPassword)
    {
        var result = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        return result;
    }
}