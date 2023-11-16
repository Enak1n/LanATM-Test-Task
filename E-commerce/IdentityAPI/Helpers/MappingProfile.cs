using AutoMapper;
using IdentityAPI.DataBase.Entities;
using IdentityAPI.Models.Requests;
using IdentityAPI.Models.Responses;
using IdentityAPI.Models;
using IdentityAPI.Models.DTO.Requests;

namespace IdentityAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterDTORequest, User>().ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<RegisterCourierDTORequest, User>().ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<UserDTORequest, User>();
            CreateMap<User, UserDTOResponse>();
            CreateMap<User, CourierDTO>();
        }
    }
}
