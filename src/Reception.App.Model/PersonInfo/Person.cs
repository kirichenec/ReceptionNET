using Reception.App.Model.Base;
using Reception.App.Model.Extensions;
using Reception.Model.Interface;

namespace Reception.App.Model.PersonInfo
{
    public class Person : BaseModel, IPerson<Post>
    {
        #region Fields
        private string _firstName;
        private string _middleName;
        private Post _post;
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

        public Post Post
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
            Post = null;
            SecondName = null;

            Comment = null;
            Id = 0;
        }

        public override bool IsEmpty()
        {
            return
                base.IsEmpty()
                && string.IsNullOrWhiteSpace(FirstName)
                && string.IsNullOrWhiteSpace(MiddleName)
                && Post.IsNullOrEmpty()
                && string.IsNullOrWhiteSpace(SecondName);
        }
        #endregion
    }
}