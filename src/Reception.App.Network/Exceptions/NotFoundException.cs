using System.Runtime.Serialization;

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

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ExceptionObject = (T)info.GetValue(nameof(ExceptionObject), typeof(T));
        }

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
}
