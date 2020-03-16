using Newtonsoft.Json;
using Reception.Extensions.Converters;
using System;

namespace Reception.Model.Network
{
    public class QueryResult<T> : IQueryResult<T>
    {
        public T Data { get; set; }
        [JsonConverter(typeof(TypeConverter))]
        public Type DataType { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}