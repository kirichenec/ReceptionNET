using AutoMapper;
using Reception.Server.File.Entities;
using Reception.Server.File.Model.Dto;

namespace Reception.Server.File.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FileData, FileDataDto>().ReverseMap();
        }
    }
}