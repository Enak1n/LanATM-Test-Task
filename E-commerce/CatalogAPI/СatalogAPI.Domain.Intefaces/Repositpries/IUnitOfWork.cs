using СatalogAPI.Domain.Intefaces.Repositpries;

namespace CatalogAPI.Domain.Intefaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IBrandRepository Brands { get; }
        ICategoryRepository Categories { get; }

        Task SaveChangesAsync();
    }
}
