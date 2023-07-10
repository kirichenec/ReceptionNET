namespace Reception.Extension
{
    public static class NullableExtensions
    {
        public static bool HasValue<T>(this T value) where T : class
        {
            return !value.HasNoValue();
        }

        public static bool HasNoValue<T>(this T value) where T : class
        {
            return value == null;
        }
    }
}