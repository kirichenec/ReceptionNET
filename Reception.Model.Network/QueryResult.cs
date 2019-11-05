namespace Reception.Model.Network
{
    public class QueryResult<T>
    {
        public T Data { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum ErrorCode
    {
        Ok,
        NotFound
    }
}