namespace CatalogAPI.Domain.Entities
{
    public class Product : EntityBase
    {
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public bool IsSale { get; set; } = false;

        public Brand? Brand { get; set; }
        public Category? Category { get; set; }
    }
}
