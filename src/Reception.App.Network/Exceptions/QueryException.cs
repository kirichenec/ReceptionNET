using System.Net;
using System.Runtime.Serialization;

namespace Reception.App.Network.Exceptions
{

    [Serializable]
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

        protected QueryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        protected QueryException(SerializationInfo info, StreamingContext context, HttpStatusCode httpStatusCode) : base(info, context)
        {
            StatusCode = httpStatusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
