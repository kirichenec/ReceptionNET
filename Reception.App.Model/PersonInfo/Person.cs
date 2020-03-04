using Reception.App.Model.Base;

namespace Reception.App.Model.PersonInfo
{
    public class Person : BaseModel
    {
        #region Fields
        private string _firstName;
        private string _middleName;
        private string _photoPath;
        private string _post;
        private string _secondName;
        #endregion

        #region ctors
        public Person() { }

        public Person(Person value, bool fullLoad = true)
        {
            if (value == null)
            {
                return;
            }
            FirstName = value.FirstName;
            MiddleName = value.MiddleName;
            PhotoPath = value.PhotoPath;
            Post = value.Post;
            SecondName = value.SecondName;

            if (!fullLoad)
            {
                return;
            }
            Comment = value.Comment;
            Id = value.Id;
        }
        #endregion

        #region Properties
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value)
                {
                    return;
                }
                _firstName = value;
                NotifyPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (_middleName == value)
                {
                    return;
                }
                _middleName = value;
                NotifyPropertyChanged();
            }
        }

        public string PhotoPath
        {
            get => _photoPath;
            set
            {
                if (_photoPath == value)
                {
                    return;
                }
                _photoPath = value;
                NotifyPropertyChanged();
            }
        }

        public string Post
        {
            get => _post;
            set
            {
                if (_post == value)
                {
                    return;
                }
                _post = value;
                NotifyPropertyChanged();
            }
        }

        public string SecondName
        {
            get => _secondName;
            set
            {
                if (_secondName == value)
                {
                    return;
                }
                _secondName = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Methods
        public void CopyFrom(Person value, bool fullCopy = true)
        {
            if (value == null)
            {
                Clear();
                return;
            }
            FirstName = value.FirstName;
            MiddleName = value.MiddleName;
            PhotoPath = value.PhotoPath;
            Post = value.Post;
            SecondName = value.SecondName;

            if (!fullCopy)
            {
                return;
            }
            Comment = value.Comment;
            Id = value.Id;
        }

        public void Clear()
        {
            FirstName = null;
            MiddleName = null;
            PhotoPath = null;
            Post = null;
            SecondName = null;

            Comment = null;
            Id = 0;
        }

        public virtual bool IsEmpty()
        {
            return
                string.IsNullOrWhiteSpace(Comment)
                && string.IsNullOrWhiteSpace(FirstName)
                && string.IsNullOrWhiteSpace(MiddleName)
                && string.IsNullOrWhiteSpace(Post)
                && string.IsNullOrWhiteSpace(SecondName);
        }
        #endregion
    }
}