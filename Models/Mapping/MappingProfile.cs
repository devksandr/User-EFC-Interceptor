using AutoMapper;
using User_EFC_Interceptor.Models.DTO;
using User_EFC_Interceptor.Models.Entities;

namespace User_EFC_Interceptor.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAddDTO, User>();
        }
    }
}
