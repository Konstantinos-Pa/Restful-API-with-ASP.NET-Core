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
    public class DriverController : Controller
    {
        private readonly IDriverRepository _driverRepository;

        public DriverController( IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Driver>> GetDrivers(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await _driverRepository.GetDrivers(sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Driver>> CreateDrivers(Driver Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _driverRepository.InsertDrivers(Entity,sqlConnectionFactory);
                Driver driver = await _driverRepository.GetDriverId(id, sqlConnectionFactory);
                return Created("/FoodDeliveryapi", driver);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Driver>> ChangeDrivers(int _driver_id, Driver Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _driverRepository.UpdateDrivers(_driver_id, Entity,sqlConnectionFactory);
                Driver driver = await _driverRepository.GetDriverId(_driver_id, sqlConnectionFactory);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDrivers(int _driver_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _driverRepository.DeleteDrivers(_driver_id, sqlConnectionFactory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
