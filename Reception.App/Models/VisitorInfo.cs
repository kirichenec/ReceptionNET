using System;

namespace Reception.App.Models
{
    public class VisitorInfo : Person
    {
        private DateTime _incomingDate;
        private string _message;

        public VisitorInfo() { }

        public VisitorInfo(Person value)
        {
            CopyFrom(value);
        }

        public DateTime IncomingDate
        {
            get => _incomingDate;
            set
            {
                if (_incomingDate == value)
                {
                    return;
                }
                _incomingDate = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                if (_message == value)
                {
                    return;
                }
                _message = value;
                NotifyPropertyChanged();
            }
        }

        public override bool IsEmpty()
        {
            return
                base.IsEmpty()
                && string.IsNullOrWhiteSpace(Message);
        }
    }
}