using System.Globalization;

namespace Reception.Extension
{
    public static class StringExtensions
    {
        public static string AsLike(this string value)
        {
            return $"%{value}%";
        }

        public static string CutEndBy(this string value, string cuttedValue)
        {
            if (value != null && cuttedValue != null && value.EndsWith(cuttedValue))
            {
                value = value[..^cuttedValue.Length];
            }
            return value;
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool ParseBool(this string value)
        {
            return bool.Parse(value);
        }

        public static int ParseInt(this string value)
        {
            return int.Parse(value);
        }

        public static string ToJoinString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        public static string ToTitleCase(this string value, CultureInfo cultureInfo = null)
        {
            return (cultureInfo ?? CultureInfo.CurrentCulture).TextInfo.ToTitleCase(value);
        }
    }
}