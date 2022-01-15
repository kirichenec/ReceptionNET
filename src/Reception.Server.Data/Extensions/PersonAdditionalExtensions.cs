using Reception.Model.Dto;
using Reception.Server.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Reception.Server.Data.Extensions
{
    public static class PersonAdditionalExtensions
    {
        public static PersonAdditionalDto ToDto(this PersonAdditional value)
        {
            return
                value == null ?
                null :
                new PersonAdditionalDto
                {
                    Id = value.Id,
                    PhotoId = value.PhotoId,
                };
        }

        public static List<PersonAdditionalDto> ToDto(this List<PersonAdditional> valueList)
        {
            return valueList.Select(value => value.ToDto()).ToList();
        }
    }
}
