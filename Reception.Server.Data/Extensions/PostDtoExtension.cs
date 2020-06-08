using Reception.Model.Dto;
using Reception.Model.Interfaces;

namespace Reception.Server.Data.Extensions
{
    public static class PostDtoExtension
    {
        public static PostDto ToDto(this IPost value)
        {
            return new PostDto { Comment = value.Comment, Id = value.Id, Name = value.Name };
        }
    }
}