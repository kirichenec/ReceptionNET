using Reception.App.Model.Base;
using Reception.Model.Interface;

namespace Reception.App.Model.PersonInfo
{
    public class Post : BaseModel, IPost
    {
        public string Name { get; set; }

        public override bool IsEmpty()
        {
            return
                base.IsEmpty()
                && string.IsNullOrWhiteSpace(Name);
        }
    }
}