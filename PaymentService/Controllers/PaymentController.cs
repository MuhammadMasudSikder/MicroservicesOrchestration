using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedContracts.Events;
using System;
using System.Threading.Tasks;

namespace PaymentService.Controllers
{
    /*
     * This listens for OrderPlaced, processes payment, and publishes PaymentProcessed.
        🔹 Listens for: OrderPlaced
        🔹 Publishes: PaymentProcessed
     */

    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        //[HttpPost("{orderId}")]
        //public async Task<IActionResult> ProcessPayment(Guid orderId)
        //{
        //    Console.WriteLine($"Processing payment for Order: {orderId}...");

        //    // Simulate payment process
        //    await Task.Delay(1000);

        //    // Publish PaymentProcessed event
        //    await _publishEndpoint.Publish(new PaymentProcessed(orderId));

        //    return Ok($"Payment processed for Order {orderId}");
        //}
    }

}
