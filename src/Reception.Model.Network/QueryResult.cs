using Newtonsoft.Json;
using Reception.Extensions.Converters;
using System;
using System.Threading.Tasks;

namespace Reception.Model.Network
{
    public class QueryResult<T>
    {
        public QueryResult() { }

        public QueryResult(T data)
        {
            Data = data;
            DataType = data.GetType();
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

        public async static Task<QueryResult<T>> ToQueryResultAsync<T>(this Task<T> valueTasked)
        {
            return (await valueTasked).ToQueryResult();
        }
    }
}