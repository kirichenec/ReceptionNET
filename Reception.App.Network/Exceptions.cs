using System;

namespace Reception.App.Network.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public T ExceptionObject { get; set; }

        public NotFoundException(string message) : base(message) { }
    }

    public class QueryException : Exception
    {
        public QueryException(string message) : base(message) { }
    }
}