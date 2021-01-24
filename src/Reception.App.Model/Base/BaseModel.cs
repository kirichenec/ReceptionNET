using Reception.Model.Interface;

namespace Reception.App.Model.Base
{
    public class BaseModel : PropertyChangedModel, IUnique, ICommentable
    {
        private string _comment;
        private int _id;

        public string Comment
        {
            get { return _comment; }
            set
            {
                if (_comment == value)
                {
                    return;
                }
                _comment = value;
                NotifyPropertyChanged();
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                NotifyPropertyChanged();
            }
        }

        public virtual bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Comment);
        }
    }
}