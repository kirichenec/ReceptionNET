using System;

namespace Reception.App.Network.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public T ExceptionObjectType { get; set; }

        public NotFoundException(string message) : base(message) { }
    }
}