namespace EVDMS.DataAccessLayer.Const;

public static class Const
{
    public static class NameOfClasses
    {
        public const string User = nameof(User);
        public const string Role = nameof(Role);
        public const string Dealer = nameof(Dealer);
        public const string Vehicle = nameof(Vehicle);
        public const string Inventory = nameof(Inventory);
        public const string Config = nameof(Config);
    }

    public static string GetSuccessMessage(string className)
    {
        return $"{className} retrieved successfully.";
    }

    public static string GetNotFoundMessage(string className)
    {
        return $"{className} not found.";
    }

    public static string GetCreateSuccessMessage(string className)
    {
        return $"{className} created successfully.";
    }

    public static string GetUpdateSuccessMessage(string className)
    {
        return $"{className} updated successfully.";
    }

    public static string GetDeleteSuccessMessage(string className)
    {
        return $"{className} deleted successfully.";
    }

    public static string GetAlreadyExistsMessage(string className)
    {
        return $"{className} already exists.";
    }
}