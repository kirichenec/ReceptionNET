using Newtonsoft.Json;
using Reception.Extension.Converters;

namespace Reception.Model.Network
{
    public class QueryResult<T>
    {
        public QueryResult() { }

        public QueryResult(T data)
        {
            Data = data;
            DataType = typeof(T);
        }

        public T Data { get; set; }

        [JsonConverter(typeof(TypeConverter))]
        public Type DataType { get; set; }
    }

    public static class QueryResultExtension
    {
        public static QueryResult<T> ToQueryResult<T>(this T value)
        {
            return new QueryResult<T>(value);
        }
    }
}