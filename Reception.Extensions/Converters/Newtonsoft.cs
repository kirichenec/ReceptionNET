using Newtonsoft.Json;

namespace Reception.Extensions.Converters
{
    public static class Newtonsoft
    {
        public static T DeserializeMessage<T>(this object value)
        {
            return JsonConvert.DeserializeObject<T>(value.ToString());
        }
    }
}
