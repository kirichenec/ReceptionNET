using Reception.App.Models;
using Reception.Model.Dto;

namespace Reception.App.Network
{
    public static class PersonExtension
    {
        //public static void RaisePropertyChanging<TSender>(this TSender This, [CallerMemberName] string propertyName = null) where TSender : IReactiveObject;
        public static void FromDto(this Person This, PersonDto personDto)
        {
            This.Comment = personDto.Comment;
            This.FirstName = personDto.FirstName;
            This.MiddleName = personDto.MiddleName;
            This.PhotoPath = personDto.PhotoPath;
            This.Post = personDto.Post;
            This.SecondName = personDto.SecondName;
            This.Uid = personDto.Uid;
        }
    }
}
