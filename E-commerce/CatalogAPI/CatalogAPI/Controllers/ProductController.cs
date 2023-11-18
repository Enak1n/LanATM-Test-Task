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
    [Produces("application/json")]
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

        /// <summary>
        /// Get all products form data base
        /// </summary>
        /// <returns>Return list of products</returns>
        /// <response code="200">Returns the list of all products</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Products.GetAllAsync());
        }

        /// <summary>
        /// Get product by id from data base
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Status about getting product</returns>
        /// <response code="200">Return product</response>  
        /// <response code="400">Return error message</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _productService.GetById(id);
                var response = _mapper.Map<ProductDTOResponse>(res);

                return Ok(response);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get product by name from data base
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Status about getting product</returns>
        /// <response code="200">Return product</response>  
        /// <response code="400">Return error message</response>  
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="model">New product</param>
        /// <returns>Status about creating new product</returns>
        /// <response code="200">Return created product</response>  
        /// <response code="400">Return error message</response>  
        /// <response code="409">Some properties already exist</response>  
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ProductDTORequest model)
        {
            try
            {
                Product product = _mapper.Map<Product>(model);

                var res = await _productService.Create(product);
                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<ProductDTOResponse>(res);

                return Ok(response);
            }
            catch (UniqueException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">Product</param>
        /// <returns>Status about updating product</returns>
        /// <response code="200">Return success message</response>  
        /// <response code="404">Product wasn't found</response>  
        /// <response code="409">Some properties already exist</response>  
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, ProductDTORequest model)
        {
            try
            {
                Product product = _mapper.Map<Product>(model);
                product.Id = id;

                await _productService.Update(product);
                await _unitOfWork.SaveChangesAsync();

                return Ok("You updated your product!");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UniqueException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
