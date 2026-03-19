using Reception.Model.Dto;
using Reception.Server.Data.Entities;

namespace Reception.Server.Data.Mapper;

public static class PersonDtoMapper
{
    public static IEnumerable<PersonDto> Map(this IEnumerable<Person> source)
    {
        return source.Select(Map);
    }

    public static PersonDto Map(this Person source)
    {
        return new()
        {
            Comment = source.Comment,
            Id = source.Id,
            FirstName = source.FirstName,
            MiddleName = source.MiddleName,
            PhotoId = source.AdditionalInfo?.PhotoId,
            Post = source.Post?.Map(),
            SecondName = source.SecondName,
        };
    }
}
