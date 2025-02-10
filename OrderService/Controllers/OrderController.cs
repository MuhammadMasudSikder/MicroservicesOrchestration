using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedContracts.Events;
using System;
using System.Threading.Tasks;

namespace OrderService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            var orderId = Guid.NewGuid();
            await _bus.Publish<ICreateOrder>(new
            {
                OrderId = orderId,
                Product = request.Product,
                Price = request.Price
            });

            return Ok(new { orderId, status = "Order Created" });
        }

        //private readonly IPublishEndpoint _publishEndpoint;

        //public OrderController(IPublishEndpoint publishEndpoint)
        //{
        //    _publishEndpoint = publishEndpoint;
        //}

        //[HttpPost("place")]
        //public async Task<IActionResult> PlaceOrder(OrderRequest request)
        //{
        //    var orderId = Guid.NewGuid();
        //    await _publishEndpoint.Publish(new OrderPlaced(orderId, request.Amount));

        //    return Ok(new { Message = "Order Placed", OrderId = orderId });
        //}
    }

    public class OrderRequest
    {
        //public decimal Amount { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
    }

}
