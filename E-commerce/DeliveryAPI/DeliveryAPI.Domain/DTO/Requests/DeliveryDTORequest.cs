namespace DeliveryAPI.Domain.DTO.Requests
{
    public class DeliveryDTORequest
    {
        public Guid CourierId { get; set; }
        public Guid AddressId { get; set; }
    }
}
