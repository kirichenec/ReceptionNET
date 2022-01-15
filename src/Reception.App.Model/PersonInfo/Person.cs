using Reception.App.Model.Base;
using Reception.App.Model.Extensions;
using Reception.Extension;

namespace Reception.App.Model.PersonInfo
{
    public class Person : BaseModel
    {
        #region ctors

        public Person() { }

        #endregion

        #region Properties

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public int PhotoId { get; set; }

        public Post Post { get; set; }

        public string SecondName { get; set; }

        #endregion

        #region Methods

        public void CopyFrom(Person value)
        {
            if (value.IsNullOrEmpty())
            {
                Clear();
                return;
            }
            FirstName = value.FirstName;
            MiddleName = value.MiddleName;
            Post = value.Post;
            SecondName = value.SecondName;
            Comment = value.Comment;
            Id = value.Id;
            PhotoId = value.PhotoId;
        }

        public void Clear()
        {
            FirstName = null;
            MiddleName = null;
            Post = null;
            SecondName = null;
            Comment = null;
            Id = 0;
            PhotoId = 0;
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