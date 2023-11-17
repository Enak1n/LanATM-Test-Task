using AutoMapper;
using CatalogAPI.Domain.DTO;
using CatalogAPI.Domain.Entities;
using CatalogAPI.Domain.Exceptions;
using CatalogAPI.Domain.Intefaces.Repositories;
using CatalogAPI.Infrastructure.Business;
using CatalogAPI.Service.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IUnitOfWork unitOfWork, IBrandService brandService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _brandService = brandService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all brands from data base
        /// </summary>
        /// <returns>List of brands</returns>
        /// <response code="200">Returns the list of all brands</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Brands.GetAllAsync());
        }

        /// <summary>
        /// Get brand by id from data base
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Return brand</returns>
        /// <response code="200">Returns the brand</response>
        /// <response code="404">Brand with this id not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _brandService.GetById(id);
                var response = _mapper.Map<BrandDTOResponse>(res);

                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Get brand by name from data base
        /// </summary>
        /// <param name="name">Name of brand</param>
        /// <returns>Brand with current name</returns>
        /// <response code="200">Returns the brand</response>
        /// <response code="404">Brand with this name not found</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var res = await _brandService.GetByName(name);

                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Create new brand 
        /// </summary>
        /// <param name="model">Brand request</param>
        /// <returns>Status about creating brand</returns>
        /// <response code="200">Successfully created</response>
        /// <response code="400">Brand already exist</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(BrandDTORequest model)
        {
            try
            {
                Brand brand = _mapper.Map<Brand>(model);

                await _brandService.Create(brand);
                await _unitOfWork.SaveChangesAsync();

                return Ok("Brand succsessfully created!");
            }
            catch (UniqueException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Update existing brand in data base
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="model">Brand request</param>
        /// <returns>Status about updating brand</returns>
        /// <response code="200">Successfully update</response>
        /// <response code="400">Brand not found</response>
        /// <response code="409">Brand already exist</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, BrandDTORequest model)
        {
            try
            {
                var brand = _mapper.Map<Brand>(model);
                brand.Id = id;

                await _brandService.Update(brand);
                await _unitOfWork.SaveChangesAsync();

                return Ok("You updated your brand!");
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
