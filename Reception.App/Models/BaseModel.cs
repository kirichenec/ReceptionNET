namespace Reception.App.Models
{
    public class BaseModel : PropertyChangedModel
    {
        private string _comment;
        private int _uid;

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

        public int Uid
        {
            get { return _uid; }
            set
            {
                if (_uid == value)
                {
                    return;
                }
                _uid = value;
                NotifyPropertyChanged();
            }
        }
    }
}