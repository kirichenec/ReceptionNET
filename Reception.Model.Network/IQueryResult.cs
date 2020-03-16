using Newtonsoft.Json;
using Reception.Extensions.Converters;
using System;

namespace Reception.Model.Network
{
    public interface IQueryResult<T>
    {
        public T Data { get; set; }
        [JsonConverter(typeof(SystemTypeConverter))]
        public Type DataType { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum ErrorCode
    {
        Ok,
        NotFound
    }
}
