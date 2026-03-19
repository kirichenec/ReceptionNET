using Reception.Model.Network;
using Reception.Server.Auth.Entities;

namespace Reception.Server.Auth.Mapper;

public static class AuthenticateResponseMapper
{
    public static AuthenticateResponse Map(this User source, string token)
    {
        return new()
        {
            FirstName = source.FirstName,
            Id = source.Id,
            LastName = source.LastName,
            Login = source.Login,
            MiddleName = source.MiddleName,
            Token = token,
        };
    }
}
