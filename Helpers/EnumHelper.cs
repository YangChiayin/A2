namespace Chiayin_Yang_Assignment2.Helpers
{
    public static class EnumHelper
    {
        public static string ToFormattedString(this Enum value)
        {
            // Get the name of the enum value
            var name = value.ToString();
            // Insert a space before each uppercase letter (except the first one)
            var formattedName = string.Concat(name.Select((c, i) => i > 0 && char.IsUpper(c) ? " " + c : c.ToString()));
            return formattedName;
        }
    }
}
