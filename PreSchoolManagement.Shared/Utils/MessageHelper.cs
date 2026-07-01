namespace PreSchoolManagement.Shared.Utils;

public static class MessageHelper
{
    public static string Added(string entityName) => $"{entityName} added successfully.";
    public static string Updated(string entityName) => $"{entityName} updated successfully.";
    public static string Deleted(string entityName) => $"{entityName} deleted successfully.";
    public static string Retrieved(string entityName) => $"{entityName} retrieved successfully.";
    public static string AlreadyExists(string entityName) => $"{entityName} already exists.";
    public static string NotFound(string entityName) => $"{entityName} not found.";
    public static string InvalidId(string entityName) => $"Invalid {entityName.ToLowerInvariant()} id.";
}
