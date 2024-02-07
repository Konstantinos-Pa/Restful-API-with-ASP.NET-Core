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
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController( IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Menu>> GetMenu(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await _menuRepository.GetMenus( sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Menu>> CreateMenu(Menu Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _menuRepository.InsertMenu(Entity,sqlConnectionFactory);
                Menu menu = await _menuRepository.GetMenuId(id, sqlConnectionFactory);
                return Created("/FoodDeliveryapi", menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Menu>> ChangeMenu(int _menu_id, Menu Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _menuRepository.UpdateMenu(_menu_id, Entity, sqlConnectionFactory);
                Menu menu = await _menuRepository.GetMenuId(_menu_id, sqlConnectionFactory);
                return Ok(menu);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMenu(int _menu_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _menuRepository.DeleteMenu(_menu_id, sqlConnectionFactory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
