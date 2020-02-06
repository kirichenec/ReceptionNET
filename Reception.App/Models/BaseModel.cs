using Reception.Model.Interfaces;

namespace Reception.App.Models
{
    public class BaseModel : PropertyChangedModel, IUnique
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
    }
}