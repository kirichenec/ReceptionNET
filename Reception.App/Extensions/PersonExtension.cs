using Reception.App.Models;

namespace Reception.App.Extensions
{
    public static class PersonExtension
    {
        public static bool IsNull(this Person value)
        {
            return value == null;
        }

        public static bool IsNullOrEmpty(this Person value)
        {
            return value.IsNull() || value.IsEmpty();
        }
    }
}