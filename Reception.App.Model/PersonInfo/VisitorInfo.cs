using System;

namespace Reception.App.Model.PersonInfo
{
    public class Visitor : Person
    {
        private DateTime _incomingDate;
        private string _message;

        public Visitor() { }

        public Visitor(Person value)
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