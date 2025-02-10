using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedContracts.Events;
using System;
using System.Threading.Tasks;

namespace ShippingService.Controllers
{
    /*
     * This listens for StockReserved, schedules shipping, and publishes ShippingScheduled.
        🔹 Listens for: StockReserved
        🔹 Publishes: ShippingScheduled
     */

    [Route("api/shipping")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ShippingController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> ScheduleShipping(Guid orderId)
        {
            Console.WriteLine($"Scheduling shipping for Order: {orderId}...");

            // Simulate shipping process
            await Task.Delay(1000);

            //// Publish ShippingScheduled event
            //await _publishEndpoint.Publish(new ShippingScheduled(orderId));

            return Ok($"Shipping scheduled for Order {orderId}");
        }
    }

}
