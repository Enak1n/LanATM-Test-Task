using AutoMapper;
using CatalogAPI.Domain.DTO;
using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Exceptions;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Service.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IProductService productService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Products.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _productService.GetById(id);

                return Ok(res);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var res = await _productService.GetByName(name);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTORequest model)
        {
            try
            {

                Product product = _mapper.Map<Product>(model);

                var res = await _productService.Create(product);
                await _unitOfWork.SaveChangesAsync();

                return Ok(res);
            }
            catch (UniqueException ex)
            {
                return Conflict(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
