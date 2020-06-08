namespace Reception.Extensions
{
    public static class StringExtensions
    {
        public static string CutEndBy(this string value, string cuttedValue)
        {
            if (value != null && cuttedValue != null && value.EndsWith(cuttedValue))
            {
                value = value.Substring(0, value.Length - cuttedValue.Length);
            }
            return value;
        }

        public static string AsLike(this string value)
        {
            return $"%{value}%";
        }
    }
}