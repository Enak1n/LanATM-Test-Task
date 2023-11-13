using AutoMapper;
using IdentityAPI.Domain.Entities;
using IdentityAPI.Models.Requests;
using IdentityAPI.Service.Models.Requests;

namespace IdentityAPI.Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistration, User>();
            CreateMap<AddressDTORequest, Address>();
        }
    }
}
