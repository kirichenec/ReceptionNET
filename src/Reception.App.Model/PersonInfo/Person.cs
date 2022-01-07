using Reception.App.Model.Base;
using Reception.App.Model.Extensions;
using Reception.Extension;
using Reception.Model.Interface;

namespace Reception.App.Model.PersonInfo
{
    public class Person : BaseModel, IPerson<Post>
    {
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

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string SecondName { get; set; }

        public Post Post { get; set; }

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
                && FirstName.IsNullOrWhiteSpace()
                && MiddleName.IsNullOrWhiteSpace()
                && Post.IsNullOrEmpty()
                && SecondName.IsNullOrWhiteSpace();
        }

        #endregion
    }
}