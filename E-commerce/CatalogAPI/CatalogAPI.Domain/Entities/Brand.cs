namespace CatalogAPI.Domain.Entities
{
    public class Brand : EntityBase
    {
        public Guid CategoryId { get; set; }
        public string PictureUrl { get; set; }
    }
}
