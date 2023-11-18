namespace DeliveryAPI.Domain.Entities
{
    public class Delivery
    {
        public Guid Id { get; set; }
        public Guid CourierId { get; set; }
        public Guid AddressId { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool IsDelivered { get; set; } = false;
        public bool IsLate { get; set; } = false;
        public bool IsPickUp = false;
    }
}
