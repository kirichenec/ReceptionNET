using Reception.Model.Dto;
using Reception.Server.Data.Entities;

namespace Reception.Server.Data.Mapper;

public static class PostDtoMapper
{
    public static PostDto Map(this Post source)
    {
        return new()
        {
            Comment = source.Comment,
            Id = source.Id,
            Name = source.Name,
        };
    }

    public static Post Map(this PostDto source)
    {
        return new()
        {
            Comment = source.Comment,
            Id = source.Id,
            Name = source.Name,
        };
    }
}
