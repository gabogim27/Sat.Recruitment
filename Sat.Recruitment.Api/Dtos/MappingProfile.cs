using AutoMapper;
using Sat.Recruitment.Domain.Entities;

namespace Sat.Recruitment.Api.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
