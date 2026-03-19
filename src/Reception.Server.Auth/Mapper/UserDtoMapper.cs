using Reception.Model.Dto;
using Reception.Server.Auth.Entities;

namespace Reception.Server.Auth.Mapper;

public static class UserDtoMapper
{
    public static IEnumerable<UserDto> Map(this IEnumerable<User> source)
    {
        return source.Select(Map);
    }

    public static UserDto Map(this User source)
    {
        return new()
        {
            FirstName = source.FirstName,
            Id = source.Id,
            LastName = source.LastName,
            Login = source.Login,
            MiddleName = source.MiddleName,
            Password = source.Password,
        };
    }

    public static User Map(this UserDto source)
    {
        return new()
        {
            FirstName = source.FirstName,
            Id = source.Id,
            LastName = source.LastName,
            Login = source.Login,
            MiddleName = source.MiddleName,
            Password = source.Password,
        };
    }
}
