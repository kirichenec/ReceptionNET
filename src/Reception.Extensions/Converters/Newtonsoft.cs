using Newtonsoft.Json;

namespace Reception.Extension.Converters
{
    public static class Newtonsoft
    {
        public static T DeserializeMessage<T>(this object value)
        {
            return value.ToString().DeserializeMessage<T>();
        }

        public static T DeserializeMessage<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string ToJsonString<T>(this T value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static IEnumerable<string> ToJsonStrings<T>(this T[] values)
        {
            return values?.AsEnumerable().ToJsonStrings();
        }

        public static IEnumerable<string> ToJsonStrings<T>(this IEnumerable<T> values)
        {
            return values?.Select(value => value.ToJsonString());
        }
    }
}