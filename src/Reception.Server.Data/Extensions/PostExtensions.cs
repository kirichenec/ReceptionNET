using Reception.Model.Dto;
using Reception.Server.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Reception.Server.Data.Extensions
{
    public static class PostExtensions
    {
        public static PostDto ToDto(this Post value)
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

        public static List<PostDto> ToDto(this List<Post> valueList)
        {
            return valueList.Select(value => value.ToDto()).ToList();
        }
    }
}