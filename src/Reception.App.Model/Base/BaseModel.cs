using Reception.Extension;
using Reception.Model.Interface;

namespace Reception.App.Model.Base
{
    public class BaseModel : PropertyChangedModel, IUnique, ICommentable
    {
        public string Comment { get; set; }

        public int Id { get; set; }

        public virtual bool IsEmpty()
        {
            return Comment.IsNullOrWhiteSpace();
        }
    }
}