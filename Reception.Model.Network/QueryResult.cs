using Newtonsoft.Json;
using Reception.Extensions.Converters;
using System;

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
}