using PropertyChanged;
using Reception.App.Model.Base;
using Reception.Extension;
using Reception.Model.Interface;

namespace Reception.App.Model.PersonInfo
{
    public class Post : BaseModel, IPost
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