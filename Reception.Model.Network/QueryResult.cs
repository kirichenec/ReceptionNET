using System;

namespace Reception.Model.Network
{
    public class QueryResult<T> : IQueryResult<T>
    {
        public T Data { get; set; }
        public Type DataType { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}