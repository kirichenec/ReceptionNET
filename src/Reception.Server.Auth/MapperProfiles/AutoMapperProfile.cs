using AutoMapper;
using Reception.Model.Dto;
using Reception.Model.Network;
using Reception.Server.Auth.Entities;

namespace Reception.Server.Auth.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public const string TOKEN_OPTION_NAME = nameof(AuthenticateResponse.Token);

        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, AuthenticateResponse>()
                .ForMember(ar => ar.Token, opt => opt.Ignore());
        }
    }
}