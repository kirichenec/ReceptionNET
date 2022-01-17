using Reception.Extension;
using System;

namespace Reception.App.Model.PersonInfo
{
    public class Visitor : Person
    {
        #region ctor

        public Visitor() { }

        public Visitor(Person value)
        {
            CopyFrom(value);
        }

        #endregion

        #region Properties

        public byte[] ImageSource { get; set; }

        public DateTime IncomingDate { get; set; }

        public string Message { get; set; }

        #endregion

        #region Methods

        public override void CopyFrom(Person value)
        {
            base.CopyFrom(value);
            ImageSource = null;
        }

        public override bool IsEmpty()
        {
            return
                base.IsEmpty()
                && Message.IsNullOrWhiteSpace();
        }

        #endregion
    }
}