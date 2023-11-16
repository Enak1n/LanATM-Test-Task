namespace OrderAPI.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set;}
    }
}
