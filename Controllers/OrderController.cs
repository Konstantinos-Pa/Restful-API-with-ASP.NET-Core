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
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController( IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Order>> CreateOrders(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await _orderRepository.GetOrders(sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> ChangeOrders(Order Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _orderRepository.InsertOrders(Entity, sqlConnectionFactory);
                Order order = await _orderRepository.GetOrderId(id, sqlConnectionFactory);
                return Created("/FoodDeliveryapi", order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> PutOrders(int _order_id, Order Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _orderRepository.UpdateOrders(_order_id, Entity,sqlConnectionFactory);
                Order order = await _orderRepository.GetOrderId(_order_id, sqlConnectionFactory);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteOrders(int _order_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _orderRepository.DeleteOrders(_order_id, sqlConnectionFactory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
