using Reception.Model.Dto;

namespace Reception.Server.Model
{
    public static class PersonExtension
    {
        public static void FromDto(this Person This, PersonDto personDto)
        {
            This.Comment = personDto.Comment;
            This.FirstName = personDto.FirstName;
            This.MiddleName = personDto.MiddleName;
            This.PhotoPath = personDto.PhotoPath;
            This.Post = personDto.Post?.Name;
            This.SecondName = personDto.SecondName;
            This.Uid = personDto.Id;
        }

        public static Person PersonFromDto(PersonDto personDto)
        {
            return
                personDto == null ?
                null :
                new Person
                {
                    Comment = personDto.Comment,
                    FirstName = personDto.FirstName,
                    MiddleName = personDto.MiddleName,
                    PhotoPath = personDto.PhotoPath,
                    Post = personDto.Post?.Name,
                    SecondName = personDto.SecondName,
                    Uid = personDto.Id
                };
        }
    }
}