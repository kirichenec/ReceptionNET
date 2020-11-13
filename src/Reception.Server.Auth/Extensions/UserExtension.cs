using Reception.Server.Auth.Entities;
using Reception.Server.Auth.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Extensions
{
    public static class UserExtension
    {
        public static UserDto ToDto(this User value)
        {
            return
                value == null ?
                null :
                new UserDto
                {
                    Id = value.Id,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    Password = value.Password,
                    Username = value.Username
                };
        }

        public static async Task<UserDto> ToDtoAsync(this Task<User> valueTasked)
        {
            return (await valueTasked).ToDto();
        }

        public static IEnumerable<UserDto> ToDtos(this IEnumerable<User> valueList)
        {
            return valueList?.Select(value => value.ToDto()).ToList();
        }

        public static async Task<IEnumerable<UserDto>> ToDtosAsync(this Task<IEnumerable<User>> valueListTasked)
        {
            return (await valueListTasked)?.Select(value => value.ToDto()).ToList();
        }
    }
}