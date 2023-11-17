using AutoMapper;
using OrderAPI.Domain.DTO.Requests;
using OrderAPI.Domain.DTO.Responses;
using OrderAPI.Domain.Entities;

namespace OrderAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Order, OrderDTOResponse>();
            CreateMap<OrderDTOResponse, Order>();
            CreateMap<OrderDTORequest, Order>();
            CreateMap<Order, OrderDTORequest>();

            CreateMap<Address, AddressDTORequest>();
            CreateMap<AddressDTORequest, Address>();
        }
    }
}
