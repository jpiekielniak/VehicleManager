namespace CarManagement.Shared.Enums;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());

        var attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Name;

        return value.ToString();
    }
}