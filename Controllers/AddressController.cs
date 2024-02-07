using FoodDeliveryapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodDeliveryapi.Repository;
using FoodDeliveryapi.Services;
using Microsoft.AspNetCore.Http;
using System;

namespace FoodDeliveryapi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController( IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Address>> GetAddress(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await _addressRepository.GetAddresses(sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Address>> CreateAddress(Address Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _addressRepository.InsertAddress(Entity, sqlConnectionFactory);
                Address address = await _addressRepository.GetAddressId(id, sqlConnectionFactory);
                return Created("/FoodDeliveryapi", address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Address>> ChangeAddress(int _address_id, Address Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _addressRepository.UpdateAddress(_address_id, Entity, sqlConnectionFactory);
                Address address = await _addressRepository.GetAddressId(_address_id, sqlConnectionFactory);
                return Ok(address);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAddress(int _address_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _addressRepository.DeleteAddress(_address_id, sqlConnectionFactory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
