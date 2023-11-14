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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unitOfWork.Brands.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _brandService.GetById(id);
                var response = _mapper.Map<BrandDTOResponse>(res);

                return Ok(response);
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
                var res = await _brandService.GetByName(name);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
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
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPut]
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
