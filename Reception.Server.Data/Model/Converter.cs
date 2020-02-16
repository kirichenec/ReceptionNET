using Reception.Model.Dto;

namespace Reception.Server.Data.Model
{
    public class Converter
    {
        public Person PersonFromDto(PersonDto personDto)
        {
            return
                new Person
                {
                    Comment = personDto.Comment,
                    FirstName = personDto.FirstName,
                    MiddleName = personDto.MiddleName,
                    PhotoPath = personDto.PhotoPath,
                    Post = personDto.Post?.Name,
                    SecondName = personDto.SecondName,
                    Id = personDto.Id
                };
        }
    }
}