using PropertyChanged;
using Reception.App.Model.Base;
using Reception.Extension;

namespace Reception.App.Model.PersonInfo
{
    public class Post : BaseModel
    {
        [DoNotNotify]
        public string Name { get; set; }

        public override bool IsEmpty()
        {
            return
                base.IsEmpty()
                && Name.IsNullOrWhiteSpace();
        }
    }
}