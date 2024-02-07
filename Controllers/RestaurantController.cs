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
    public class RestaurantController : Controller
    {

        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Restaurant>> GetRestaurants(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await _restaurantRepository.GetRestaurants(sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Restaurant>> CreateRestaurants(Restaurant Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _restaurantRepository.InstertRestaurants(Entity,sqlConnectionFactory);
                Restaurant restaurant = await _restaurantRepository.GetRestaurantId(id, sqlConnectionFactory);

                return Created("/FoodDeliveryapi", restaurant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Restaurant>> ChangeRestaurants(int _restaurant_id, Restaurant Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _restaurantRepository.UpdateRestaurants(_restaurant_id, Entity,sqlConnectionFactory);
                Restaurant restaurant = await _restaurantRepository.GetRestaurantId(_restaurant_id, sqlConnectionFactory);

                return Ok(restaurant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRestaurants(int _restaurant_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _restaurantRepository.DeleteRestaurants(_restaurant_id, sqlConnectionFactory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
