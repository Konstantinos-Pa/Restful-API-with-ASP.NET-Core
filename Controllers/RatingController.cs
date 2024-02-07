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
    public class RatingController : Controller
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingController( IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Rating>> GetRating(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await  _ratingRepository.GetRatings(sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rating>> CreateRating(Rating Entity,SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _ratingRepository.InsertRating(Entity,sqlConnectionFactory);
                Rating rating = await _ratingRepository.GetRatingId(id, sqlConnectionFactory);
                return Created("/FoodDeliveryapi", rating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Rating>> ChangeRating(int _rating_id, Rating Entity,SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _ratingRepository.UpdateRating(_rating_id, Entity,sqlConnectionFactory);
                Rating rating = await _ratingRepository.GetRatingId(_rating_id, sqlConnectionFactory);
                return Ok(rating);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRating(int _rating_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _ratingRepository.DeleteRating(_rating_id, sqlConnectionFactory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
