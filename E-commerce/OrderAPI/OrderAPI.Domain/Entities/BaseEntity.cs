namespace OrderAPI.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
