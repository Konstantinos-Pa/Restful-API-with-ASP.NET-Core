using Microsoft.AspNetCore.Mvc;
using FoodDeliveryapi.Models;
using FoodDeliveryapi.Repository;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using FoodDeliveryapi.Services;
using Microsoft.AspNetCore.Http;
using System;


namespace FoodDeliveryapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IConfiguration configuration,IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<User>> GetUser(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await _userRepository.GetUsers(sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<User>> CreateUser(User Entity,SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _userRepository.InsertUsers(Entity, sqlConnectionFactory);
                User user = await _userRepository.GetUserId(id, sqlConnectionFactory);

                return Created("/FoodDeliveryapi", user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> ChangeUser(int _user_id, User Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _userRepository.UpdateUsers(_user_id, Entity,sqlConnectionFactory);
                User user = await _userRepository.GetUserId(_user_id,sqlConnectionFactory);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUsers(int _user_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _userRepository.DeleteUsers(_user_id, sqlConnectionFactory);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
