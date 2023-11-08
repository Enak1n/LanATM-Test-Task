namespace CatalogAPI.Domain.Entities
{
    public class Product : EntityBase
    {
        public double Price { get; set; }
        public Category Category { get; set; }
        public bool IsSale { get; set; }
    }
}
