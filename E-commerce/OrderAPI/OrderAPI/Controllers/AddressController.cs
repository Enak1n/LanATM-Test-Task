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
    [Produces("application/json")]
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

        /// <summary>
        /// Get list of addresses
        /// </summary>
        /// <returns>Status about getting addresses</returns>
        /// <response code="200">Return the list of all addresses</response>
        /// <response code="400">Return the error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Get address from data base by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Status about getting address</returns>
        /// <response code="200">Return the address</response>
        /// <response code="404">Address with current id not found</response>
        /// <response code="400">Return the error while getting address</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Get address from data base by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Status about creating address</returns>
        /// <response code="200">Return the address</response>
        /// <response code="400">Return the error while creating address</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
