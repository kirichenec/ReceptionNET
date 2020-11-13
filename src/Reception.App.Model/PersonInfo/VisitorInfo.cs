using System;

namespace Reception.App.Model.PersonInfo
{
    public class Visitor : Person
    {
        #region Fields
        private DateTime _incomingDate;
        private string _message;
        private byte[] _imageSource;
        #endregion

        #region ctor
        public Visitor() { }

        public Visitor(Person value)
        {
            CopyFrom(value);
        }
        #endregion

        #region Properties
        public byte[] ImageSource
        {
            get => _imageSource;
            set
            {
                if (_imageSource == value)
                {
                    return;
                }
                _imageSource = value;
                NotifyPropertyChanged();
            }
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
        #endregion

        #region Methods
        public override bool IsEmpty()
        {
            return
                base.IsEmpty()
                && string.IsNullOrWhiteSpace(Message);
        }
        #endregion
    }
}