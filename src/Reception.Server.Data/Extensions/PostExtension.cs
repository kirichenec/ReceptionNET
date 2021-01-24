using Reception.Model.Dto;
using Reception.Model.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Reception.Server.Data.Extensions
{
    public static class PostExtension
    {
        public static PostDto ToDto(this IPost value)
        {
            return
                value == null ?
                null :
                new PostDto
                {
                    Comment = value.Comment,
                    Id = value.Id,
                    Name = value.Name
                };
        }

        public static List<PostDto> ToDto(this List<IPost> valueList)
        {
            return valueList.Select(value => value.ToDto()).ToList();
        }
    }
}