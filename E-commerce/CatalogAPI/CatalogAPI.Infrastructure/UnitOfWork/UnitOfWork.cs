using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Infrastructure.DataBase;
using CatalogAPI.Infrastructure.UnitOfWork.Repositories;
using СatalogAPI.Domain.Intefaces.Repositpries;

namespace CatalogAPI.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Context _context;

        public IProductRepository Products { get; private set; }

        public IBrandRepository Brands { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
            Products = new ProductRepository(context);
            Brands = new BrandRepository(context);
            Categories = new CategoryRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
