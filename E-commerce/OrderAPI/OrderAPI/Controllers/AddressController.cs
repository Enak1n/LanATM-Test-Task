using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Domain.DTO.Requests;
using OrderAPI.Domain.Entities;
using OrderAPI.Domain.Exceptions;
using OrderAPI.Domain.Interfaces;
using OrderAPI.Service.Interfaces;

namespace OrderAPI.Controllers
{
    [Authorize(Policy = "AccessToAddress")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IUnitOfWork unitOfWork, IAddressService addressService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var addresses = await _addressService.GetAll();

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var res = await _addressService.GetById(id);
                return Ok(res);
            }
            catch(NotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddressDTORequest addressDTOrequest)
        {
            try
            {
                var address = _mapper.Map<Address>(addressDTOrequest);
                var createdAddress = await _addressService.Create(address);

                return Ok(createdAddress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
