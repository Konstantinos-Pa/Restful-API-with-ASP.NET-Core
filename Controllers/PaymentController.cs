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
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController( IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Payment>> GetPayment(SQLConnectionFactory sqlConnectionFactory)
        {
            return Ok(await _paymentRepository.GetPayments(sqlConnectionFactory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Payment>> CreatePayment(Payment Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                int id = await _paymentRepository.InsertPayment(Entity,sqlConnectionFactory);
                Payment payment = await _paymentRepository.GetPaymentId(id, sqlConnectionFactory);
                return Created("/FoodDeliveryapi", payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Payment>> ChangePayment(int _payment_id, Payment Entity, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _paymentRepository.UpdatePayment(_payment_id, Entity,sqlConnectionFactory);
                Payment payment = await _paymentRepository.GetPaymentId(_payment_id, sqlConnectionFactory);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePayment(int _payment_id, SQLConnectionFactory sqlConnectionFactory)
        {
            try
            {
                await _paymentRepository.DeletePayment(_payment_id, sqlConnectionFactory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
