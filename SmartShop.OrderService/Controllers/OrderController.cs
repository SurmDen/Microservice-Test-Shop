using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartShop.OrderService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace SmartShop.OrderService.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController : ControllerBase
    {
        private IOrderRepository OrderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            OrderRepository = orderRepository;
        }

        [HttpGet("All")]
        public IActionResult GetOrders()
        {
            if (OrderRepository.GetOrders().Count() != 0)
            {
                return Ok(OrderRepository.GetOrders());
            }

            return Ok("Order list is fall");
        }


        [HttpGet("Make")]
        public IActionResult MakeOrder(string address)
        {
            if (!string.IsNullOrEmpty(address))
            {
                OrderRepository.MakeOrder(address);
                return Ok("The order created");
            }
            return BadRequest();
            
        }


        [HttpDelete("Remove/{id}")]
        public IActionResult DeleteOrder(long id)
        {
            OrderRepository.DeleteOrder(id);
            return Ok("deleted");
        }


        [HttpPost("Ship/{id}")]
        public IActionResult ShipOrder(long id)
        {
            OrderRepository.ShipOrder(id);
            return Ok("shiped");
        }
    }
}
