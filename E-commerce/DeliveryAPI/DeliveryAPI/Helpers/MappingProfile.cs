using AutoMapper;
using DeliveryAPI.Domain.DTO.Requests;
using DeliveryAPI.Domain.Entities;
using Rabbit.DTO;

namespace DeliveryAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeliveryDTORequest, Delivery>();
            CreateMap<Delivery, DeliveryDTORequest>();

            CreateMap<Courier, CourierRabbitDTO>();
            CreateMap<CourierRabbitDTO, Courier>();

            CreateMap<DeliveryRabbitDTO, Delivery>();
            CreateMap<Delivery, DeliveryRabbitDTO>();
        }
    }
}
