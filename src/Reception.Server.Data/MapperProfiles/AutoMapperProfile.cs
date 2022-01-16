using AutoMapper;
using Reception.Model.Dto;
using Reception.Server.Data.Entities;

namespace Reception.Server.Data.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();

            CreateMap<Person, PersonDto>()
                .ForMember(nameof(PersonDto.PhotoId), opt => opt.MapFrom(p => p.AdditionalInfo.PhotoId));
        }
    }
}