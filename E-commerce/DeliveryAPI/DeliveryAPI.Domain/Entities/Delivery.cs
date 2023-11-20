namespace DeliveryAPI.Domain.Entities
{
    public class Delivery
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public Guid? CourierId { get; set; }
        public Courier? Courier { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
        public bool IsDelivered { get; set; } = false;
        public bool IsLate { get; set; } = false;
        public bool IsPickUp { get; set; } = false;
        public bool IsCancel { get; set; } = false;
        public bool IsComplete { get; set; } = false;
    }
}
