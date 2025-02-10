using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedContracts.Events;
using System;
using System.Threading.Tasks;

namespace InventoryService.Controllers
{
    /*
     This listens for PaymentProcessed, reserves stock, and publishes StockReserved.
    🔹 Listens for: PaymentProcessed
    🔹 Publishes: StockReserved
     */
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public InventoryController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> ReserveStock(Guid orderId)
        {
            Console.WriteLine($"Reserving stock for Order: {orderId}...");

            // Simulate stock reservation
            await Task.Delay(1000);

            //// Publish StockReserved event
            //await _publishEndpoint.Publish(new StockReserved(orderId));

            return Ok($"Stock reserved for Order {orderId}");
        }
    }

}
