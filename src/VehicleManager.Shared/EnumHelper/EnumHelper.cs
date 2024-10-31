namespace VehicleManager.Shared.EnumHelper;

public static class EnumHelper
{
    public static List<KeyValuePair<int, string>> GetEnumValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => new KeyValuePair<int, string>(Convert.ToInt32(e), GetEnumDisplayName(e)))
            .ToList();
    }

    private static string GetEnumDisplayName<T>(T value) where T : Enum
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var displayAttribute = fieldInfo?.GetCustomAttribute<DisplayAttribute>();
        return displayAttribute != null ? displayAttribute.Name : value.ToString();
    }
}