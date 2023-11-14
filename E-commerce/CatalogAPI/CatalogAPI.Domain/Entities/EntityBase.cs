namespace CatalogAPI.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}