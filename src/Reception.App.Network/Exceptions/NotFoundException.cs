namespace Reception.App.Network.Exceptions
{
    public class NotFoundException<T> : Exception
    {
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


        public T ExceptionObject { get; }
    }
}
