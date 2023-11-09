namespace CatalogAPI.Domain.Entities
{
    public class Product : EntityBase
    {
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public bool IsSale { get; set; } = false;
    }
}
