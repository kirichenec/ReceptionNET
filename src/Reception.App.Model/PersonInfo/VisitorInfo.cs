using Reception.Extension;

namespace Reception.App.Model.PersonInfo
{
    public class Visitor : Person
    {
        public Visitor() { }

        public Visitor(Person value)
        {
            CopyFrom(value);
        }


        public Guid Guid { get; set; }

        public byte[] ImageSource { get; set; }

        public DateTime IncomingDate { get; set; }

        public string Message { get; set; }


        public override void Clear()
        {
            base.Clear();
            ImageSource = null;
            Message = null;
        }

        public override bool IsEmpty()
        {
            return
                base.IsEmpty()
                && Message.IsNullOrWhiteSpace();
        }

        public void SetIncomingInformation()
        {
            IncomingDate = DateTime.Now;
            Guid = Guid.NewGuid();
        }
    }
}