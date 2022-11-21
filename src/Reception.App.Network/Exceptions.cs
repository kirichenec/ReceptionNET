using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Reception.App.Network.Exceptions
{
    [Serializable]
    public class NotFoundException<T> : Exception
    {
        public T ExceptionObject { get; }

        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public NotFoundException(string message, T exceptionObject) : base(message)
        {
            ExceptionObject = exceptionObject;
        }

        public NotFoundException(string message, T exceptionObject, Exception innerException) : base(message, innerException)
        {
            ExceptionObject = exceptionObject;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ExceptionObject = (T)info.GetValue(nameof(ExceptionObject), typeof(T));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(ExceptionObject), ExceptionObject);

            base.GetObjectData(info, context);
        }
    }

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