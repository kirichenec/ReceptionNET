using System;

namespace Reception.App.Models
{
    class VisitorInfo : Person
    {
        private DateTime _incomingDate;
        private string _message;

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
    }
}
