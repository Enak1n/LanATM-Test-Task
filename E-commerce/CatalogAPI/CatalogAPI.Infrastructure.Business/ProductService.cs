using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Service.Intefaces;
using CatalogAPI.Domain.Exceptions;

namespace CatalogAPI.Infrastructure.Business
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> Create(Product product)
        {
            var res = await _unitOfWork.Products.FindAsync(x => x.Name == product.Name);

            if (res != null)
            {
                throw new UniqueException($"Product with name {product.Name} already exist!");
            }

            if (_unitOfWork.Brands.GetByIdAsync(product.BrandId) == null)
            {
                throw new NotFoundException($"Brand with id {product.BrandId} not found!");
            }

            if (await _unitOfWork.Categories.FindAsync(x => x.Id == product.CategoryId) == null)
            {
                throw new NotFoundException($"Category with id {product.CategoryId} not found!");
            }

            await _unitOfWork.Products.AddAsync(product);
            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            var res = _unitOfWork.Products.GetByIdAsync(id);

            if(res == null)
                throw new NotFoundException($"Brand with Id {id} not found!");

            return res;
        }

        public async Task<Product> GetByName(string name)
        {
            var res = await _unitOfWork.Products.FindAsync(x => x.Name ==  name);

            if(res == null)
                throw new NotFoundException($"Product with name {name} not found!");

            return res;
        }

        public async Task Update(Product product)
        {
            var res = _unitOfWork.Products.GetByIdAsync(product.Id);

            if ((res.Name != product.Name) && await _unitOfWork.Products.FindAsync(x => x.Name == product.Name) != null)
            {
                throw new UniqueException($"Product with name {product.Name} already exists!");
            }

            if (res == null)
            {
                throw new NotFoundException($"Product with Id {product.Id} was not founded!");
            }

            if (_unitOfWork.Brands.GetByIdAsync(product.BrandId) == null)
            {
                throw new NotFoundException($"Brand with Id {product.BrandId} was not founded!");
            }

            if (_unitOfWork.Categories.GetByIdAsync(product.CategoryId) == null)
            {
                throw new NotFoundException($"Category with Id {product.CategoryId} was not founded!");
            }

            await _unitOfWork.Products.EditAsync(product);
        }
    }
}
