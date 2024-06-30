using System.Net;

namespace Reception.App.Network.Exceptions
{
    public class QueryException : Exception
    {
        public QueryException() { }

        public QueryException(string message) : base(message) { }

        public QueryException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            StatusCode = httpStatusCode;
        }

        public QueryException(string message, Exception innerException) : base(message, innerException) { }

        public QueryException(string message, Exception innerException, HttpStatusCode httpStatusCode) : base(message, innerException)
        {
            StatusCode = httpStatusCode;
        }


        public HttpStatusCode StatusCode { get; }
    }
}
