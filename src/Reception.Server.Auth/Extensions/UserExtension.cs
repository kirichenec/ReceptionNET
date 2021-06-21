using Reception.Model.Dto;
using Reception.Model.Network;
using Reception.Server.Auth.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reception.Server.Auth.Extensions
{
    public static class UserExtension
    {
        public static AuthenticateResponse ToAuthenticateResponse(this UserDto user, string token)
        {
            return
                user == null
                ? null
                : new AuthenticateResponse
                {

                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Login = user.Login,
                    MiddleName = user.MiddleName,
                    Token = token
                };
        }

        public static UserDto ToDto(this User value)
        {
            return
                value == null
                ? null
                : new UserDto
                {
                    Id = value.Id,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    Login = value.Login,
                    MiddleName = value.MiddleName,
                    Password = value.Password
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